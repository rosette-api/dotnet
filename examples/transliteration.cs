using System;
using System.Collections.Generic;
using rosette_api;

namespace examples
{
    class transliteration
    {
        /// <summary>
        /// Example code to call Rosette API to transliterate a name.
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
                string transliteration_data = "ana r2ye7 el gam3a el sa3a 3 el 3asr";

                TransliterationEndpoint endpoint = new TransliterationEndpoint(transliteration_data).SetLanguage("ara");
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
