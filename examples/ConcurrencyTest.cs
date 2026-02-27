using Rosette.Api.Client;

namespace Rosette.Api.Examples;

public class ConcurrencyTest {
    private static readonly int threads = 3;
    private static readonly int calls = 5;

    /// <summary>
    /// RunEndpoint runs the example.  By default the endpoint will be run against the Rosette Cloud Service.
    /// An optional alternate URL may be provided, i.e. for an on-premise solution.
    /// This particular example runs entities on multiple threads to demonstrate
    /// concurrency.
    /// </summary>
    /// <param name="apiKey">Required api key (obtained from Basis Technology)</param>
    /// <param name="altUrl">Optional alternate URL</param>
    private static async Task TestConcurrency(string apiKey, string altUrl) {
        var tasks = new List<Task>();
        ApiClient api = new(apiKey);
        if (!string.IsNullOrEmpty(altUrl)) {
            api.UseAlternateURL(altUrl);
        }
        api = api.AssignConcurrentConnections(2); // 2 is the default. Set to the amount allowed by your plan

        foreach (int task in Enumerable.Range(0, threads)) {
            Console.WriteLine("Starting task {0}", task);
            tasks.Add(Task.Factory.StartNew( () => RunLookup(task, api) ));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine("Test complete");
    }

    private static Task RunLookup(int taskId, ApiClient api) {
        string entities_text_data = @"The Securities and Exchange Commission today announced the leadership of the agency’s trial unit.  Bridget Fitzpatrick has been named Chief Litigation Counsel of the SEC and David Gottesman will continue to serve as the agency’s Deputy Chief Litigation Counsel. Since December 2016, Ms. Fitzpatrick and Mr. Gottesman have served as Co-Acting Chief Litigation Counsel.  In that role, they were jointly responsible for supervising the trial unit at the agency’s Washington D.C. headquarters as well as coordinating with litigators in the SEC’s 11 regional offices around the country.";
        Rosette.Api.Client.Endpoints.Entities endpoint = new(entities_text_data);
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

    /// <summary>
    /// Main is a simple entrypoint for command line calling of the endpoint examples
    /// </summary>
    /// <param name="args">Command line args, expects API Key, (optional) alt URL</param>
    static void Main(string[] args) {
        if (args.Length != 0) {
            TestConcurrency(args[0], args.Length > 1 ? args[1] : null).GetAwaiter().GetResult();
        }
        else {
            Console.WriteLine("An API Key is required");
        }
    }
}