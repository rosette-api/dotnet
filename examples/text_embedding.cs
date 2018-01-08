using System;
using System.Collections.Generic;
using rosette_api;

namespace examples
{
    class text_embedding
    {
        /// <summary>
        /// Example code to call Rosette API to get a document's text-embedding.
        /// Requires Nuget Package:
        /// rosette_api
        /// </summary>
        static void Main(string[] args)
        {
            //To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            //You may set the API key via command line argument:
            //text-embedding yourapiKeyhere
            if (args.Length != 0)
            {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }
            try
            {
                RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey).UseAlternateURL(altUrl);
                string embedding_data = @"Cambridge, Massachusetts";
                TextEmbeddingEndpoint endpoint = new TextEmbeddingEndpoint(embedding_data);
                RosetteResponse response = endpoint.Call(api);
                foreach (KeyValuePair<string, string> h in response.Headers)
                {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }

                Console.WriteLine(response.ContentAsJson(pretty: true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
