﻿using System;
using System.Collections.Generic;
using rosette_api;

namespace examples {
    class morphology_han_readings {
        /// <summary>
        /// RunEndpoint runs the example.  By default the endpoint will be run against the Rosette Cloud Service.
        /// An optional alternate URL may be provided, i.e. for an on-premise solution.
        /// </summary>
        /// <param name="apiKey">Required api key (obtained from Basis Technology)</param>
        /// <param name="altUrl">Optional alternate URL</param>
        private void RunEndpoint(string apiKey, string altUrl=null) {
            try
            {
                RosetteAPI api = new RosetteAPI(apiKey);
                if (!string.IsNullOrEmpty(altUrl)) {
                    api.UseAlternateURL(altUrl);
                }
                string morphology_han_readings_data = @"北京大学生物系主任办公室内部会议";
                MorphologyEndpoint endpoint = new MorphologyEndpoint(morphology_han_readings_data, MorphologyFeature.hanReadings);
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
                new morphology_han_readings().RunEndpoint(args[0], args.Length > 1 ? args[1] : null);
            }
            else {
                Console.WriteLine("An API Key is required");
            }
        }
    }
}
