using Rosette.Api;
using Rosette.Api.Models;

namespace examples {
    class Events
    {
        /// <summary>
        /// RunEndpoint runs the example.  By default the endpoint will be run against the Rosette Cloud Service.
        /// An optional alternate URL may be provided, i.e. for an on-premise solution.
        /// </summary>
        /// <param name="apiKey">Required api key (obtained from Basis Technology)</param>
        /// <param name="altUrl">Optional alternate URL</param>
        private void RunEndpoint(string apiKey, string? altUrl = null) {
            try {
                ApiClient api = new ApiClient(apiKey);
                if (!string.IsNullOrEmpty(altUrl)) {
                    api.UseAlternateURL(altUrl);
                }
                string events_text_data = @"Bill Gates went to the store.";

                Rosette.Api.Endpoints.Events endpoint = new Rosette.Api.Endpoints.Events(events_text_data);
                Response response = endpoint.Call(api);

                // Print out the response headers
                foreach (KeyValuePair<string, string> h in response.Headers) {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }
                // Print out the content in JSON format.  The Content property returns an IDictionary.
                Console.WriteLine(response.ContentAsJson(pretty: true));

                // Retrieve the Events with full ADM
                response = endpoint.SetUrlParameter("output", "rosette").Call(api);
                Console.WriteLine(response.ContentAsJson(pretty: true));
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        /// <summary>
        /// Main is a simple entrypoint for command line calling of the endpoint examples
        /// </summary>
        /// <param name="args">Command line args, expects API Key, (optional) alt URL</param>
        static void Main(string[] args) {
            if (args.Length != 0) {
                new Events().RunEndpoint(args[0], args.Length > 1 ? args[1] : null);
            }
            else {
                Console.WriteLine("An API Key is required");
            }
        }
    }
}