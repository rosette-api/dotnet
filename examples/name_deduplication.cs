﻿using System;
using System.Collections.Generic;
using System.Linq;
using rosette_api;

namespace examples {
    class name_deduplication {
        /// <summary>
        /// RunEndpoint runs the example.  By default the endpoint will be run against the Rosette Cloud Service.
        /// An optional alternate URL may be provided, i.e. for an on-premise solution.
        /// </summary>
        /// <param name="apiKey">Required api key (obtained from Basis Technology)</param>
        /// <param name="altUrl">Optional alternate URL</param>
        private void RunEndpoint(string apiKey, string altUrl=null) {
            try {
                RosetteAPI api = new RosetteAPI(apiKey);
                if (!string.IsNullOrEmpty(altUrl)) {
                    api.UseAlternateURL(altUrl);
                }
                string name_dedupe_data = @"Alice Terry,Alice Thierry,Betty Grable,Betty Gable,Norma Shearer,Norm Shearer,Brigitte Helm,Bridget Helem,Judy Holliday,Julie Halliday";

                List<string> dedupe_names = name_dedupe_data.Split(',').ToList<string>();
                List<RosetteName> names = dedupe_names.Select(name => new RosetteName(name)).ToList();

                NameDeduplicationEndpoint endpoint = new NameDeduplicationEndpoint(names).SetThreshold(0.75f);
                RosetteResponse response = endpoint.Call(api);
                foreach (KeyValuePair<string, string> h in response.Headers) {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }
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
                new name_deduplication().RunEndpoint(args[0], args.Length > 1 ? args[1] : null);
            }
            else {
                Console.WriteLine("An API Key is required");
            }
        }
    }
}
