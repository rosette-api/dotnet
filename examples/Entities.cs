using Rosette.Api.Client;
using Rosette.Api.Client.Models;

namespace examples {
    class Entities
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
                string entities_text_data = @"The Securities and Exchange Commission today announced the leadership of the agency’s trial unit.  Bridget Fitzpatrick has been named Chief Litigation Counsel of the SEC and David Gottesman will continue to serve as the agency’s Deputy Chief Litigation Counsel. Since December 2016, Ms. Fitzpatrick and Mr. Gottesman have served as Co-Acting Chief Litigation Counsel.  In that role, they were jointly responsible for supervising the trial unit at the agency’s Washington D.C. headquarters as well as coordinating with litigators in the SEC’s 11 regional offices around the country.";

                Rosette.Api.Client.Endpoints.Entities endpoint = new Rosette.Api.Client.Endpoints.Entities(entities_text_data).SetGenre("social-media");
                Response response = endpoint.Call(api);

                // Print out the response headers
                foreach (KeyValuePair<string, string> h in response.Headers) {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }
                // Print out the content in JSON format.  The Content property returns an IDictionary.
                Console.WriteLine(response.ContentAsJson(pretty: true));

                // Retrieve the Entities with full ADM
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
                new Entities().RunEndpoint(args[0], args.Length > 1 ? args[1] : null);
            }
            else {
                Console.WriteLine("An API Key is required");
            }
        }
    }
}
