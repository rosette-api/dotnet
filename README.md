<a href="https://www.babelstreet.com/rosette">
<picture>
  <source media="(prefers-color-scheme: light)" srcset="https://charts.babelstreet.com/icon-dark.png">
  <source media="(prefers-color-scheme: dark)" srcset="https://charts.babelstreet.com/icon-light.png">
  <img alt="Babel Street Logo" width="48" height="48">
</picture>
</a>

# Analytics by Babel Street

---

[![NuGet version](https://badge.fury.io/nu/rosette_api.svg)](https://badge.fury.io/nu/rosette_api)

Our product is a full text processing pipeline from data preparation to extracting the most relevant information and
analysis utilizing precise, focused AI that has built-in human understanding. Text Analytics provides foundational
linguistic analysis for identifying languages and relating words. The result is enriched and normalized text for
high-speed search and processing without translation.

Text Analytics extracts events and entities — people, organizations, and places — from unstructured text and adds the
structure of associating those entities into events that deliver only the necessary information for near real-time
decision making. Accompanying tools shorten the process of training AI models to recognize domain-specific events.

The product delivers a multitude of ways to sharpen and expand search results. Semantic similarity expands search
beyond keywords to words with the same meaning, even in other languages. Sentiment analysis and topic extraction help
filter results to what’s relevant.

## Analytics API Access
- Analytics Cloud [Sign Up](https://developer.babelstreet.com/signup)

## Migrating from C# Binding

There are breaking changes between the C# Binding and the Dotnet Binding.  In order to improve concurrency each endpoint is now its own class and requires an api object in order to execute a call.  Additionally, required parameters are enforced in the constructor, while optional parameters are provided through a builder pattern.

Example (C# Binding):

Scene: On-Premise retrieval of entities with full ADM

``` csharp
  string alturl = "localhost:1234/rest/v1";
  CAPI api = new CAPI(apikey, alturl);
  // C# sets the url parameter at the api level, affecting all endpoints until reset
  api.SetUrlParameter("output", "rosette");

  string entities_text_data = @"The Securities and Exchange Commission today announced the leadership of the agency’s trial unit.  Bridget Fitzpatrick has been named Chief Litigation Counsel of the SEC and David Gottesman will continue to serve as the agency’s Deputy Chief Litigation Counsel. Since December 2016, Ms. Fitzpatrick and Mr. Gottesman have served as Co-Acting Chief Litigation Counsel.  In that role, they were jointly responsible for supervising the trial unit at the agency’s Washington D.C. headquarters as well as coordinating with litigators in the SEC’s 11 regional offices around the country.";

  EntitiesResponse response = api.Entity(entities_text_data, null, null, null, "social-media");
  foreach (KeyValuePair<string, string> h in response.Headers) {
      Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
  }
  Console.WriteLine(response.ToString());
```

Dotnet Binding:

``` csharp
  string altUrl = "localhost:1234/rest/v1";
  // The alternate URL is set via a method, rather than through the constructor
  RosetteAPI api = new RosetteAPI(apiKey).UseAlternateURL(altUrl);

  string entities_text_data = @"The Securities and Exchange Commission today announced the leadership of the agency’s trial unit.  Bridget Fitzpatrick has been named Chief Litigation Counsel of the SEC and David Gottesman will continue to serve as the agency’s Deputy Chief Litigation Counsel. Since December 2016, Ms. Fitzpatrick and Mr. Gottesman have served as Co-Acting Chief Litigation Counsel.  In that role, they were jointly responsible for supervising the trial unit at the agency’s Washington D.C. headquarters as well as coordinating with litigators in the SEC’s 11 regional offices around the country.";

  // Create the endpoint.  In this case we set the genre and want the full ADM.  Note that the url parameters are set for the endpoint only, so other endpoints
  // are not affected.
  EntitiesEndpoint endpoint = new EntitiesEndpoint(entities_text_data)
    .SetGenre("social-media")
    .SetUrlParameter("output", "rosette");
  RosetteResponse response = endpoint.Call(api);

  // Print out the response headers
  foreach (KeyValuePair<string, string> h in response.Headers) {
      Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
  }
  // Print out the content in JSON format.  The Content property returns an IDictionary.
  Console.WriteLine(response.ContentAsJson(pretty: true));
```

#### Synchronous
```csharp
using Rosette.Api.Client;
using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

ApiClient api = new("YOUR_API_KEY");

string text = @"The Securities and Exchange Commission today announced the leadership 
of the agency's trial unit. Bridget Fitzpatrick has been named Chief Litigation Counsel 
of the SEC.";

Entities endpoint = new Entities(text).SetGenre("social-media");
Response response = endpoint.Call(api);

foreach (KeyValuePair<string, string> h in response.Headers) {
    Console.WriteLine($"{h.Key}:{h.Value}");
}
Console.WriteLine(response.ContentAsJson(pretty: true));
```

#### Async (Recommended)
```csharp
using Rosette.Api.Client;
using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

ApiClient api = new("YOUR_API_KEY");

string text = @"The Securities and Exchange Commission today announced the leadership 
of the agency's trial unit. Bridget Fitzpatrick has been named Chief Litigation Counsel 
of the SEC.";

Entities endpoint = new Entities(text).SetGenre("social-media");

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
Response response = await endpoint.CallAsync(api, cts.Token);

Console.WriteLine(response.ContentAsJson(pretty: true));
```

## Documentation

View the [documentation](https://rosette-api.github.io/dotnet/)

## Packing the build

Packing the build will breate a nupkg file that can be used when running the examples.  This is the same file that is published to NuGet
From the root directory of the source tree, `dotnet pack`.  Note the output directory if you want to run examples against it later.  It will be something like `rosette_api/bin/Debug`.

## Publishing

TODO: Coming soon

## Building, Testing and Examples

The easiest way to build and test the source and run the examples against it is to use `rosette/docker-dotnet`

1. cd to the root of the dotnet source
2. `docker run --rm -it -e <your api key> -v $(PWD):/source rosette/docker-dotnet`

If you would like to run against an alternate URL, add `-e <alternate url>` before the `-v`

If you would prefer the manual approach, install dotnet on your computer and reference the following:

### Building the source

From the root directory of the source tree, `dotnet build`.  Note that .NET 10 SDK is a prerequisite for building the source.

### Running the tests

From the root directory of the source tree, `dotnet test tests/tests.csproj`

#### Examples
View small example programs for each Rosette endpoint
in the [examples](https://github.com/rosette-api/dotnet/tree/master/examples) directory.

#### Documentation & Support
- [Binding API](https://rosette-api.github.io/java/)
- [Analytics Platform API](https://docs.babelstreet.com/API/en/index-en.html)
- [Binding Release Notes](https://github.com/rosette-api/dotnet/wiki/Release-Notes)
- [Analytics Platform Release Notes](https://docs.babelstreet.com/Release/en/rosette-cloud.html)
- [Support](https://babelstreet.my.site.com/support/s/)
- [Binding License: Apache 2.0](LICENSE.txt)
