using Rosette.Api.Models;
using System.Collections.Specialized;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Rosette.Api.Endpoints.Core;

/// <summary>
/// EndpointProcessor provices the compilation and processing of the endpoint
/// through the server.  It includes functions that may be used by the different endpoints
/// as needed.
/// </summary>
public class EndpointExecutor {
    private const string CONTENT = "content";
    private const string CONTENTURI = "contenturi";
    private const string LANGUAGE = "language";
    private const string GENRE = "genre";
    private const string OPTIONS = "options";
    private const string HTTP_MEDIA_TYPE = "application/json";

    /// <summary>
    /// _params contains the parameters to be sent to the server
    /// </summary>
    private readonly Dictionary<string, object> _params;

    /// <summary>
    /// _options contains user provided options
    /// </summary>
    private readonly Dictionary<string, object> _options;

    /// <summary>
    /// _urlParameters is a NameValueCollection to provide URL query string parameters to the call
    /// </summary>
    private readonly NameValueCollection _urlParameters;

    /// <summary>
    /// BaseEndpoint is the constructor
    /// </summary>
    public EndpointExecutor(
    Dictionary<string, object> parameters,
    Dictionary<string, object> options,
    NameValueCollection urlParameters,
    string endpoint)
{
    ArgumentNullException.ThrowIfNull(parameters);
    ArgumentNullException.ThrowIfNull(options);
    ArgumentNullException.ThrowIfNull(urlParameters);
    ArgumentException.ThrowIfNullOrWhiteSpace(endpoint);
    
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
    public string? Language {
        get => _params.ContainsKey(LANGUAGE) ? _params[LANGUAGE].ToString() : string.Empty;
        set => _params[LANGUAGE] = value;
    }

    /// <summary>
    /// Genre returns the provided genre or an empty string
    /// </summary>
    public string? Genre {
        get => _params.ContainsKey(GENRE) ? _params[GENRE].ToString() : string.Empty;
        set => _params[GENRE] = value;
    }

    /// <summary>
    /// GetCallAsync executes the endpoint against the server using GetAsync
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response</returns>
    public async Task<Response> GetCallAsync(ApiClient api, CancellationToken cancellationToken = default)
    {
        string url = api.URI + Endpoint;
        var responseMsg = await api.Client.GetAsync(url, cancellationToken).ConfigureAwait(false);
        return await Response.CreateAsync(responseMsg).ConfigureAwait(false);
    }

    /// <summary>
    /// GetCall executes the endpoint against the server using GetAsync
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <returns>Response</returns>
    public Response GetCall(ApiClient api)
    {
        return GetCallAsync(api).GetAwaiter().GetResult();
    }

    /// PostCallAsync calls the server with the provided data using PostAsync
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response</returns>
    public virtual async Task<Response> PostCallAsync(ApiClient api, CancellationToken cancellationToken = default)
    {
        string url = api.URI + Endpoint + ToQueryString();

        if (Filestream == null)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            HttpContent content = new StringContent(
                JsonSerializer.Serialize(AppendOptions(_params), serializeOptions),
                Encoding.UTF8,
                HTTP_MEDIA_TYPE
            );

            var responseMsg = await api.Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);
            return await Response.CreateAsync(responseMsg).ConfigureAwait(false);
        }
        else
        {
            return await PostAsMultipartAsync(api, url, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// PostCall calls the server with the provided data using PostAsync
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <returns>Response</returns>
    public virtual Response PostCall(ApiClient api)
    {
        return PostCallAsync(api).GetAwaiter().GetResult();
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
            dict.Remove(OPTIONS); // Remove returns false if key doesn't exist, no need to check
        }
        return dict;
    }

    /// <summary>
    /// PostAsMultipartAsync handles processing of files as a multipart upload
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <param name="url">Endpoint URL</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response object</returns>
    private async Task<Response> PostAsMultipartAsync(ApiClient api, string url, CancellationToken cancellationToken = default)
    {
        using (var multiPartContent = new MultipartFormDataContent())
        {
            var streamContent = new StreamContent(Filestream);
            streamContent.Headers.Add("Content-Type", FileContentType);
            streamContent.Headers.Add("Content-Disposition", "mixed; name=\"content\"; filename=\"" + Path.GetFileName(Filestream.Name) + "\"");
            multiPartContent.Add(streamContent, "content", Path.GetFileName(Filestream.Name));

            if (_options.Count > 0 || _params.Count > 0)
            {
                var serializeOptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var stringContent = new StringContent(
                    JsonSerializer.Serialize(AppendOptions(_params), serializeOptions),
                    Encoding.UTF8,
                    HTTP_MEDIA_TYPE
                );
                stringContent.Headers.Add("Content-Disposition", "mixed; name=\"request\"");
                multiPartContent.Add(stringContent, "request");
            }

            var responseMsg = await api.Client.PostAsync(url, multiPartContent, cancellationToken).ConfigureAwait(false);
            return await Response.CreateAsync(responseMsg).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// PostAsMultipart handles processing of files as a multipart upload
    /// </summary>
    /// <param name="api">RosetteAPI object</param>
    /// <param name="url">Endpoint URL</param>
    /// <returns>RosetteResponse object</returns>
    private Response PostAsMultipart(ApiClient api, string url)
    {
        using (var _multiPartContent = new MultipartFormDataContent())
        {
            var streamContent = new StreamContent(Filestream);
            streamContent.Headers.Add("Content-Type", FileContentType);
            streamContent.Headers.Add("Content-Disposition", "mixed; name=\"content\"; filename=\"" + Path.GetFileName(Filestream.Name) + "\"");
            _multiPartContent.Add(streamContent, "content", Path.GetFileName(Filestream.Name));

            if (_options.Count > 0 || _params.Count > 0)
            {
                var serializeOptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var stringContent = new StringContent(
                    JsonSerializer.Serialize(AppendOptions(_params), serializeOptions),
                    Encoding.UTF8,
                    HTTP_MEDIA_TYPE
                );
                stringContent.Headers.Add("Content-Disposition", "mixed; name=\"request\"");
                _multiPartContent.Add(stringContent, "request");
            }

            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await api.Client.PostAsync(url, _multiPartContent));
            var response = task.Result;
            return new Response(response);
        }
    }

    /// <summary>
    /// clearKey removes the specified key from the _params dictionary
    /// </summary>
    /// <param name="key">key name</param>
    private void ClearKey(string key) => _params.Remove(key);

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
