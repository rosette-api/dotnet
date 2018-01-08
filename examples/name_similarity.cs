using System;
using System.Collections.Generic;
using rosette_api;

namespace examples
{
    class matched_name
    {
        /// <summary>
        /// Example code to call Rosette API to get match score (similarity) for two names.
        /// Requires Nuget Package:
        /// rosette_api
        /// </summary>
        static void Main(string[] args)
        {
            //To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            //You may set the API key via command line argument:
            //matched_name yourapiKeyhere
            if (args.Length != 0)
            {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }
            try
            {
                RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey).UseAlternateURL(altUrl);
                string matched_name_data1 = @"Michael Jackson";
                string matched_name_data2 = @"迈克尔·杰克逊";
                NameSimilarityEndpoint endpoint = new NameSimilarityEndpoint(new RosetteName(matched_name_data1), new RosetteName(matched_name_data2));
                RosetteResponse response = endpoint.Call(api);
                foreach (KeyValuePair<string, string> h in response.Headers) {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }
                Console.WriteLine(response.ContentAsJson(pretty: true));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
