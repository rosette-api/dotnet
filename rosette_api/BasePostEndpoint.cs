using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace rosette_api
{
    public class BasePostEndpoint<T> where T : BasePostEndpoint<T>
    {
        /// <summary>
        /// _params contains the parameters to be sent to the server
        /// </summary>
        private Dictionary<string, object> _params;
        /// <summary>
        /// _options contains user provided options
        /// </summary>
        private Dictionary<string, object> _options;
        /// <summary>
        /// _urlParameters is a NameValueCollection to provide URL query string parameters to the call
        /// </summary>
        private NameValueCollection _urlParameters;

        /// <summary>
        /// Endpoint returns the assigned endpoint
        /// </summary>
        public string Endpoint { get; protected set; }
        /// <summary>
        /// Filename returns the provided filename or an empty string
        /// </summary>
        public string Filename { get; protected set; }
        /// <summary>
        /// FileContentType returns the assigned Content-Type for a multipart file upload
        /// </summary>
        public string FileContentType { get; protected set; }
        /// <summary>
        /// Options returns the option dictionary
        /// </summary>
        public Dictionary<string, object> Options { get { return _options; } }
        /// <summary>
        /// UrlParameters returns any parameters to be used for query string
        /// </summary>
        public NameValueCollection UrlParameters { get { return _urlParameters;  } }

        /// <summary>
        /// Content returns the textual content, the URI or an empty string
        /// </summary>
        public string Content {
            get {
                return _params.ContainsKey("content") ? _params["content"].ToString()
                    :  _params.ContainsKey("contenturi") ? _params["contenturi"].ToString()
                    :  string.Empty;
            }
        }
        /// <summary>
        /// Language returns the provided 3-letter language code or an empty string
        /// </summary>
        public string Language {
            get {
                return _params.ContainsKey("language") ? _params["language"].ToString() : string.Empty;
            }
        }
        /// <summary>
        /// Genre returns the provided genre or an empty string
        /// </summary>
        public string Genre {
            get {
                return _params.ContainsKey("genre") ? _params["genre"].ToString() : string.Empty;
            }
        }
        /// <summary>
        /// BaseEndpoint is the constructor
        /// </summary>
        public BasePostEndpoint() {
            _params = new Dictionary<string, object>();
            _options = new Dictionary<string, object>();
            _urlParameters = new NameValueCollection();
            FileContentType = "text/plain";
        }

        /// <summary>
        /// Sets the content to be provided to the Rosette API service
        /// It will check content to see if it is a filename, then URI, then
        /// default to text.
        /// </summary>
        /// <param name="content">May be a filename, URI or textual content</param>
        /// <returns>An updated copy of this</returns>
        public T SetContent(string content) {
            if (File.Exists(content)) {
                Filename = content;
                ClearKey("content");
                ClearKey("contenturi");
            }
            else if (Uri.IsWellFormedUriString(content, UriKind.Absolute)) {
                _params["contenturi"] = content;
                ClearKey("content");
                Filename = "";
            }
            else {
                _params["content"] = content;
                ClearKey("contentUri");
                Filename = "";
            }
            return (T)this;
        }
        /// <summary>
        /// SetLanguage sets the 3-letter language code
        /// </summary>
        /// <param name="language">3-letter language code, e.g "zho"</param>
        /// <returns>Updated object</returns>
        public T SetLanguage(string language) {
            _params["language"] = language;
            return (T)this;
        }
        /// <summary>
        /// SetGenre sets the genre
        /// </summary>
        /// <param name="genre">genre</param>
        /// <returns>Updated object</returns>
        public T SetGenre(string genre) {
            _params["genre"] = genre;
            return (T)this;
        }
        /// <summary>
        /// SetFileContentType sets the Content-Type for multipart operations.
        /// </summary>
        /// <param name="contentType">Content-Type, defaults to "text/plain"</param>
        /// <returns>Updated object</returns>
        public T SetFileContentType(string contentType) {
            return (T)this;
        }
        /// <summary>
        /// SetOption sets an option and value
        /// </summary>
        /// <param name="optionName">Name of option</param>
        /// <param name="optionValue">Value of option</param>
        /// <returns>Update object</returns>
        public T SetOption(string optionName, string optionValue) {
            _options[optionName] = optionValue;

            return (T)this;
        }
        /// <summary>
        /// RemoveOption removes an option
        /// </summary>
        /// <param name="optionName">Name of option</param>
        /// <returns>Update object</returns>
        public T RemoveOption(string optionName) {
            if (_options.ContainsKey(optionName)) {
                _options.Remove(optionName);
            }

            return (T)this;
        }
        /// <summary>
        /// ClearOptions removes all options
        /// </summary>
        /// <returns>Updated object</returns>
        public T ClearOptions() {
            _options.Clear();

            return (T)this;
        }
        /// <summary>
        /// SetUrlParameter sets a key/value for a query string, e.g. ?output=rosette
        /// </summary>
        /// <param name="parameterKey">parameter key</param>
        /// <param name="parameterValue">parameter value</param>
        /// <returns>Updated object</returns>
        public T SetUrlParameter(string parameterKey, string parameterValue) {
            _urlParameters.Add(parameterKey, parameterValue);

            return (T)this;
        }
        /// <summary>
        /// RemoveUrlParameter removes all values associated with a specified key
        /// </summary>
        /// <param name="parameterKey">parameter key</param>
        /// <returns>Update object</returns>
        public T RemoveUrlParameter(string parameterKey) {
            _urlParameters.Remove(parameterKey);

            return (T)this;
        }
        /// <summary>
        /// Call calls the server with the provided data
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>Rosette Response</returns>
        public virtual RosetteResponse Call(RosetteAPI api) {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(AppendOptions(_params)));
            string url = api.URI + Endpoint + ToQueryString();
            if (string.IsNullOrEmpty(Filename)) {
                Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await api.Client.PostAsync(url, content));
                var response = task.Result;

                return new RosetteResponse(response);
            }
            else {
                return PostAsMultipart(api, url);
            }
        }
        /// <summary>
        /// AppendOptions appends any provided options to the parameter dictionary
        /// </summary>
        /// <param name="dict">Dictionary of options</param>
        /// <returns>Dictionary of string, object</returns>
        private Dictionary<string, object> AppendOptions(Dictionary<string, object> dict) {
            if (_options.Count > 0) {
                dict["options"] = _options;
            }
            return dict;
        }
        /// <summary>
        /// PostAsMultipart handles processing of files as a multipart upload
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <param name="url">Endpoint URL</param>
        /// <returns>RosetteResponse object</returns>
        private RosetteResponse PostAsMultipart(RosetteAPI api, string url) {

            using (var _multiPartContent = new MultipartFormDataContent()) {
                FileStream fs = File.OpenRead(Filename);
                var streamContent = new StreamContent(fs);
                streamContent.Headers.Add("Content-Type", FileContentType);
                streamContent.Headers.Add("Content-Disposition", "mixed; name=\"content\"; filename=\"" + Path.GetFileName(Filename) + "\"");
                _multiPartContent.Add(streamContent, "content", Path.GetFileName(Filename));

                if (_options.Count > 0 || _params.Count > 0) {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(AppendOptions(_params)), Encoding.UTF8, "application/json");
                    stringContent.Headers.Add("Content-Disposition", "mixed; name=\"request\"");
                    _multiPartContent.Add(stringContent, "request");
                }
                Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await api.Client.PostAsync(url, _multiPartContent));

                var response = task.Result;
                return new RosetteResponse(response);
            }
        }
        /// <summary>
        /// clearKey removes the specified key from the _params dictionary
        /// </summary>
        /// <param name="key">key name</param>
        private void ClearKey(string key) {
            if (_params.ContainsKey(key)) {
                _params.Remove(key);
            }
        }
        /// <summary>
        /// ToQueryString is a helper to generate the query string to append to the URL
        /// </summary>
        /// <returns>query string</returns>
        private string ToQueryString() {
            if (UrlParameters.Count == 0) {
                return String.Empty;
            }
            StringBuilder sb = new StringBuilder("?");

            bool first = true;
            foreach (string key in UrlParameters.AllKeys) {
                foreach (string value in UrlParameters.GetValues(key)) {
                    if (!first) {
                        sb.Append("&");
                    }
                    sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                    first = false;
                }
            }

            return sb.ToString();
        }
    }
}
