using System;
using System.Collections.Generic;
using rosette_api;

namespace examples
{
    class language
    {
        /// <summary>
        /// Example code to call Rosette API to detect possible languages for a piece of text.
        /// Requires Nuget Package:
        /// rosette_api
        /// </summary>
        static void Main(string[] args)
        {
            //To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            //You may set the API key via command line argument:
            //language yourapiKeyhere
            if (args.Length != 0)
            {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }
            try
            {
                // Example demonstrates the adding of a custom header
                RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey)
                    .UseAlternateURL(altUrl)
                    .AddCustomHeader("X-RosetteAPI-App", "csharp-app");

                string language_data = @"Por favor Señorita, says the man.";

                LanguageEndpoint endpoint = new LanguageEndpoint(language_data);
                //The results of the API call will come back in the form of a Dictionary
                RosetteResponse response = endpoint.Call(api);
                foreach (KeyValuePair<string, string> h in response.Headers) {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }
                Console.WriteLine(response.ContentAsJson(true));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
