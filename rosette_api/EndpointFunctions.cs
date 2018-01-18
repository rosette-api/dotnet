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
    /// <summary>
    /// EndpointProcessor provices the compilation and processing of the endpoint
    /// through the server.  It includes functions that may be used by the different endpoints
    /// as needed.
    /// </summary>
    public class EndpointFunctions {
        private const string CONTENT = "content";
        private const string CONTENTURI = "contenturi";
        private const string LANGUAGE = "language";
        private const string GENRE = "genre";
        private const string OPTIONS = "options";
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
        /// BaseEndpoint is the constructor
        /// </summary>
        public EndpointFunctions(Dictionary<string, object> parameters,
            Dictionary<string, object> options,
            NameValueCollection urlParameters,
            string endpoint) {
            _params = parameters;
            _options = options;
            _urlParameters = urlParameters;
            Endpoint = endpoint;
            FileContentType = "text/plain";
        }
        /// <summary>
        /// Endpoint returns the assigned endpoint
        /// </summary>
        public string Endpoint { get; private set; }
        /// <summary>
        /// Filename returns the provided FileStream's filename
        /// </summary>
        public string Filename { get => Filestream == null ? string.Empty : Filestream.Name; }
        /// <summary>
        /// Filestream contains the user provided FileStream or null
        /// </summary>
        /// <returns></returns>
        public FileStream Filestream {get; private set; }
        /// <summary>
        /// FileContentType returns the assigned Content-Type for a multipart file upload
        /// </summary>
        public string FileContentType { get; set; }
        /// <summary>
        /// Parameters return the paramters dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> Parameters { get => _params; }
        /// <summary>
        /// Options returns the option dictionary
        /// </summary>
        public Dictionary<string, object> Options { get=> _options; }
        /// <summary>
        /// UrlParameters returns any parameters to be used for query string
        /// </summary>
        public NameValueCollection UrlParameters { get => _urlParameters; }

        /// <summary>
        /// Content returns the textual content, the URI (as a string) or an empty string
        /// </summary>
        public object Content {
            get => _params.ContainsKey(CONTENT) ? _params[CONTENT]
                    : _params.ContainsKey(CONTENTURI) ? _params[CONTENTURI]
                    : string.Empty;
            set {
                if (value.GetType() == typeof(FileStream)) {
                    Filestream = (FileStream)value;
                    ClearKey(CONTENT);
                    ClearKey(CONTENTURI);
                }
                else if (value.GetType() == typeof(Uri)) {
                    _params[CONTENTURI] = ((Uri)value).AbsoluteUri;
                    ClearKey(CONTENT);
                    Filestream = null;
                }
                else {
                    _params[CONTENT] = value;
                    ClearKey(CONTENTURI);
                    Filestream = null;
                }
            }
        }
        /// <summary>
        /// Language returns the provided 3-letter language code or an empty string
        /// </summary>
        public string Language {
            get => _params.ContainsKey(LANGUAGE) ? _params[LANGUAGE].ToString() : string.Empty;
            set => _params[LANGUAGE] = value;
        }
        /// <summary>
        /// Genre returns the provided genre or an empty string
        /// </summary>
        public string Genre {
            get => _params.ContainsKey(GENRE) ? _params[GENRE].ToString() : string.Empty;
            set => _params[GENRE] = value;
        }
        /// <summary>
        /// GetCall executes the endpoint against the server using GetAsync
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>RosetteResponse</returns>
        public RosetteResponse GetCall(RosetteAPI api) {
            string url = api.URI + Endpoint;
            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await api.Client.GetAsync(url));
            var response = task.Result;

            return new RosetteResponse(response);
        }
        /// <summary>
        /// Call calls the server with the provided data using PostAsync
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>Rosette Response</returns>
        public virtual RosetteResponse PostCall(RosetteAPI api) {
            string url = api.URI + Endpoint + ToQueryString();
            if (Filestream == null) {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(AppendOptions(_params)));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
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
                dict[OPTIONS] = _options;
            }
            else {
                if (dict.ContainsKey(OPTIONS)) {
                    dict.Remove(OPTIONS);
                }
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
                var streamContent = new StreamContent(Filestream);
                streamContent.Headers.Add("Content-Type", FileContentType);
                streamContent.Headers.Add("Content-Disposition", "mixed; name=\"content\"; filename=\"" + Path.GetFileName(Filestream.Name) + "\"");
                _multiPartContent.Add(streamContent, "content", Path.GetFileName(Filestream.Name));

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
        /// <summary>
        /// HasContent checks for content to send
        /// </summary>
        /// <returns>bool if content, contenturi or filename</returns>
        private bool HasContent() {
            return _params.ContainsKey(CONTENT) || _params.ContainsKey(CONTENTURI) || !string.IsNullOrEmpty(Filename);
        }
    }

}
