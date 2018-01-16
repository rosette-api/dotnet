using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using rosette_api;

namespace examples
{
    class sentiment
    {
        /// <summary>
        /// Example code to call Rosette API to get a document's sentiment
        /// Requires Nuget Package:
        /// rosette_api
        /// </summary>
        static void Main(string[] args)
        {
            //To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            //You may set the API key via command line argument:
            //sentiment yourapiKeyhere
            if (args.Length != 0)
            {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }

            try
            {
                RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey).UseAlternateURL(altUrl);
                // Create a temporary file to demonstrate multi-part file upload of data
                var newFile = Path.GetTempFileName();
                StreamWriter sw = new StreamWriter(newFile);
                string sentiment_file_data = @"<html><head><title>New Ghostbusters Film</title></head><body><p>Original Ghostbuster Dan Aykroyd, who also co-wrote the 1984 Ghostbusters film, couldn’t be more pleased with the new all-female Ghostbusters cast, telling The Hollywood Reporter, “The Aykroyd family is delighted by this inheritance of the Ghostbusters torch by these most magnificent women in comedy.”</p></body></html>";
                sw.WriteLine(sentiment_file_data);
                sw.Flush();
                sw.Close();

                using (FileStream fs = File.OpenRead(newFile)) {
                    SentimentEndpoint endpoint = new SentimentEndpoint(fs)
                        .SetFileContentType(@"application/octet-stream")
                        .SetLanguage("eng");
                    RosetteResponse response = endpoint.Call(api);
                    foreach (KeyValuePair<string, string> h in response.Headers) {
                        Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                    }
                    Console.WriteLine(response.ContentAsJson(pretty: true));
                }

                if (File.Exists(newFile)) {
                    File.Delete(newFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}