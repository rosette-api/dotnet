using System;
using System.Collections.Generic;
using rosette_api;

namespace examples
{
    class morphology_lemmas
    {
        /// <summary>
        /// Example code to call Rosette API to get lemmas for words in a piece of text.
        /// Requires Nuget Package:
        /// rosette_api
        /// </summary>
        static void Main(string[] args)
        {
            //To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            //You may set the API key via command line argument:
            //morphology_lemmas yourapiKeyhere
            if (args.Length != 0)
            {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }
            try
            {
                RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey).UseAlternateURL(altUrl);
                string morphology_lemmas_data = @"The fact is that the geese just went back to get a rest and I'm not banking on their return soon";
                //The results of the API call will come back in the form of a Dictionary
                MorphologyEndpoint endpoint = new MorphologyEndpoint(morphology_lemmas_data, MorphologyFeature.lemmas);
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
