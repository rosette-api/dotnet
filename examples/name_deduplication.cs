using System;
using System.Collections.Generic;
using System.Linq;
using rosette_api;

namespace examples
{
    class name_deduplication
    {
        /// <summary>
        /// Example code to call Rosette API to deduplication a list of names.
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
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
