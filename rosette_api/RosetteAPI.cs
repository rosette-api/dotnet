using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace rosette_api
{
    public class RosetteAPI
    {
        private Dictionary<string, string> _customHeaders;

        /// <summary>
        /// APIKey is the Rosette API key provided by Basis Technology
        /// </summary>
        public string APIKey { get; private set; }

        /// <summary>
        /// Client is the HttpClient to be used for communication with the server.
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// URI is the uri of the Rosette API server.
        /// Default: https://api.rosette.com/rest/v1/
        /// </summary>
        public string URI { get; private set; }

        /// <summary>
        /// ConcurrentConnections is the maximum number of connections allowed by the Rosette API Server.
        /// Default: 2
        /// </summary>
        public int ConcurrentConnections { get; private set; }

        /// <summary>
        /// Timeout is the receive data timeout for the http client.
        /// Default: 30 seconds
        /// </summary>
        public int Timeout { get; private set; }

        /// <summary>
        /// Version returns the version of the assembly
        /// </summary>
        public static string Version {
            get => typeof(RosetteAPI).Assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Debug turns on extended debug information
        /// Default: false
        /// </summary>
        public bool Debug { get; private set; }

        /// <summary>
        /// RosetteAPI provides the connection to be used for all of the endpoints.
        /// </summary>
        /// <param name="apiKey">Required Rosette API key</param>
        public RosetteAPI(string apiKey) {
            APIKey = apiKey ?? throw new ArgumentNullException("apiKey", "The API Key cannot be null");
            URI = "https://api.rosette.com/rest/v1/";
            Client = null;
            ConcurrentConnections = 2;
            Timeout = 300;
            Debug = false;
            _customHeaders = new Dictionary<string, string>();

            Prepare();
        }

        /// <summary>
        /// UseAlternateURL allows the user to specify a different URL for connection to the Rosette API server
        /// </summary>
        /// <param name="urlString">Destination URL string</param>
        /// <returns>RosetteAPI object</returns>
        public RosetteAPI UseAlternateURL(string urlString) {
            URI = urlString.EndsWith("/") ? urlString : urlString + "/";

            return Prepare();
        }

        /// <summary>
        /// AssignClient allows the user to assign a Client object of their choosing.  Additional Rosette API headers
        /// will be added to this prior to operation.
        /// </summary>
        /// <param name="client">A valid HttpClient</param>
        /// <returns>RosetteAPI object</returns>
        public RosetteAPI AssignClient(HttpClient client) {
            Client = client;

            return Prepare();
        }

        /// <summary>
        /// AssignConcurrentConnections allows the user to specify the number of connections to use. This number, default 2,
        /// is based on the user's plan with Basis Technology.
        /// </summary>
        /// <param name="connections">Maximum number of connections to allow</param>
        /// <returns>RosetteAPI object</returns>
        public RosetteAPI AssignConcurrentConnections(int connections) {
            ConcurrentConnections = connections < 2 ? 2 : connections;

            return Prepare(true);
        }

        /// <summary>
        /// AssignTimeoutInMS sets the Client receive data timeout in seconds
        /// Default: 30 seconds
        /// </summary>
        /// <param name="timeout">Timeout in seconds</param>
        /// <returns>RosetteAPI object</returns>
        public RosetteAPI AssignTimeout(int timeout) {
            Timeout = timeout < 0 ? 0 : timeout;

            return Prepare();
        }

        /// <summary>
        /// SetDebug sets the debug state of the server response.
        /// Default: false
        /// </summary>
        /// <param name="state">Debug state</param>
        /// <returns>RosetteAPI object</returns>
        public RosetteAPI SetDebug(bool state=true) {
            Debug = state;

            return Prepare();
        }

        /// <summary>
        /// AddCustomHeader allows the user to add custom headers to the Client.  Header names must be prefixed with
        /// 'X-RosetteAPI-'
        /// </summary>
        /// <param name="headerName">Name of header, prefixed with 'X-RosetteAPI-'</param>
        /// <param name="headerValue">Value of header</param>
        /// <returns>RosetteAPI object</returns>
        public RosetteAPI AddCustomHeader(string headerName, string headerValue) {
            if (!headerName.StartsWith("X-RosetteAPI-")) {
                throw new ArgumentException("headerName", @"Custom header name must begin with 'X-RosetteAPI-'");
            }
            if (_customHeaders.ContainsKey(headerName) && headerValue == null) {
                _customHeaders.Remove(headerName);
            }
            else {
                _customHeaders[headerName] = headerValue;
            }

            return Prepare();
        }

        /// <summary>
        /// Prepare constructs the Client, with Rosette specific headers, Timeout, ConcurrentConnections, etc.
        /// If an outside client is provided, timeout, header and custom header values will still be applied.
        /// Warning: Changing the concurrent connections requires a new client, which will replace the user provided one with an
        /// internal one. It is advisable that if a user wants to control their own Http Client, they should set the concurrent
        /// connections on that object prior to assigning it to the RosetteAPI.
        /// </summary>
        /// <param name="forceUpdate">Forces the client to refresh.  This is necessary if the concurrent connections are changed.</param>
        /// <returns>RosetteAPI object</returns>
        private RosetteAPI Prepare(bool forceUpdate=false) {
            if (Client == null || forceUpdate) {
                Client =
                    new HttpClient(
                        new HttpClientHandler {
                            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                            MaxConnectionsPerServer = ConcurrentConnections,
                        });
            }
            Client.Timeout = TimeSpan.FromSeconds(Timeout);

            if (Client.BaseAddress == null) {
                Client.BaseAddress = new Uri(URI); // base address must be the rosette URI regardless of whether the client is external or internal
            }

            // Standard headers, which are required for Rosette API
            AddRequestHeader("X-RosetteAPI-Key", APIKey);
            AddRequestHeader("User-Agent", string.Format("RosetteAPICsharp/{0}-{1}", Version, Environment.Version.ToString()));
            AddRequestHeader("X-RosetteAPI-Binding", "csharp");
            AddRequestHeader("X-RosetteAPI-Binding-Version", Version);
            if (Debug) {
                AddRequestHeader("X-RosetteAPI-Devel", Debug.ToString());
            }

            var acceptHeader = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
            if (!Client.DefaultRequestHeaders.Accept.Contains(acceptHeader)) {
                Client.DefaultRequestHeaders.Accept.Add(acceptHeader);
            }

            foreach (string encodingType in new List<string>() { "gzip", "deflate" }) {
                var encodingHeader = new System.Net.Http.Headers.StringWithQualityHeaderValue(encodingType);
                if (!Client.DefaultRequestHeaders.AcceptEncoding.Contains(encodingHeader)) {
                    Client.DefaultRequestHeaders.AcceptEncoding.Add(encodingHeader);
                }
            }

            // Custom headers provided by the user
            if (_customHeaders.Count > 0) {
                foreach (KeyValuePair<string, string> entry in _customHeaders) {
                    AddRequestHeader(entry.Key, entry.Value);
                }
            }

            return this;
        }

        /// <summary>
        /// AddRequestHeader is a helper method to add a header value to the client
        /// </summary>
        /// <param name="name">header name</param>
        /// <param name="value">header value</param>
        private void AddRequestHeader(string name, string value) {
            if (!Client.DefaultRequestHeaders.Contains(name)) {
                Client.DefaultRequestHeaders.Add(name, value);
            }
        }


    }
}
