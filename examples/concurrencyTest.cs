using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using rosette_api;

namespace examples
{
public class ConcurrencyTest {
        private static int threads = 3;
        private static int calls = 5;

        static void Main(string[] args) {
            // To use the C# API, you must provide an API key
            string apiKey = "Your API key";
            string altUrl = string.Empty;

            // You may set the API key via command line argument:
            // concurrencyTest yourapiKeyhere
            if (args.Length != 0) {
                apiKey = args[0];
                altUrl = args.Length > 1 ? args[1] : string.Empty;
            }
            // Block on the test, otherwise the threads will exit before completion when main exits
            TestConcurrency(apiKey, altUrl).GetAwaiter().GetResult();
        }
        private static async Task TestConcurrency(string apiKey, string altUrl) {
            var tasks = new List<Task>();
            RosetteAPI api = string.IsNullOrEmpty(altUrl) ? new RosetteAPI(apiKey) : new RosetteAPI(apiKey).UseAlternateURL(altUrl);
            api = api.AssignConcurrentConnections(2); // 2 is the default. Set to the amount allowed by your plan

            foreach (int task in Enumerable.Range(0, threads)) {
                Console.WriteLine("Starting task {0}", task);
                tasks.Add(Task.Factory.StartNew( () => runLookup(task, api) ));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Test complete");
        }
        private static Task runLookup(int taskId, RosetteAPI api) {
            string entities_text_data = @"The Securities and Exchange Commission today announced the leadership of the agency’s trial unit.  Bridget Fitzpatrick has been named Chief Litigation Counsel of the SEC and David Gottesman will continue to serve as the agency’s Deputy Chief Litigation Counsel. Since December 2016, Ms. Fitzpatrick and Mr. Gottesman have served as Co-Acting Chief Litigation Counsel.  In that role, they were jointly responsible for supervising the trial unit at the agency’s Washington D.C. headquarters as well as coordinating with litigators in the SEC’s 11 regional offices around the country.";
            EntitiesEndpoint endpoint = new EntitiesEndpoint(entities_text_data);
            foreach (int call in Enumerable.Range(0, calls)) {
                Console.WriteLine("Task ID: {0} call {1}", taskId, call);
                try {
                    Console.WriteLine(endpoint.Call(api).ContentAsJson(pretty: true));
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
            return Task.CompletedTask;
        }
    }
}