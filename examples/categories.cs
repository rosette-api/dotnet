using System;
using System.Collections.Generic;
using rosette_api;

namespace examples
{
    class categories
    {
        /// <summary>
        /// Example code to call Rosette API to get a document's (located at given URL) categories.
        /// Requires Nuget Package:
        /// rosette_api
        /// </summary>
        static void Main(string[] args)
        {
            //To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            //You may set the API key via command line argument:
            //categories yourapiKeyhere
            if (args.Length != 0)
            {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }
            try
            {
                RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey).UseAlternateURL(altUrl);
                string categories_text_data = @"Sony Pictures is planning to shoot a good portion of the new ""Ghostbusters"" in Boston as well.";

                CategoriesEndpoint endpoint = new CategoriesEndpoint(categories_text_data);

                RosetteResponse response = endpoint.Call(api);
                //The results of the API call will come back in the form of a Dictionary
                Console.WriteLine(response.ContentAsJson(true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
