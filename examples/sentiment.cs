using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using rosette_api;

namespace examples {
    class Sentiment
    {
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
                new Sentiment().RunEndpoint(args[0], args.Length > 1 ? args[1] : null);
            }
            else {
                Console.WriteLine("An API Key is required");
            }
        }
    }
}