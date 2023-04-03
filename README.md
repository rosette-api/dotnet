# Dotnet Client Binding

Rosette API Client Library for .Net

## Summary

This is intended to be a replacement for the C# binding.  Although it is written in C#, it has the following improvements:

- targets .Net Core 2 rather than Framework
- updated to handle multi-threaded operations and address some concurrency issues in the old C# binding
- all new unit tests
- removal of brittle return types, replaced with IDictionary and JSON so that returned data reflects the latest from the server

## Usage

1. Add the rosette_api package from NuGet: `dotnet add package rosette_api.net` (it's not published yet)
1. Add `using rosette_api` to your source file
1. Create a `RosetteAPI` object to manage the HTTP client: `RosetteAPI api = new RosetteAPI(your api key);`
1. Create an endpoint object: `LanguageEndpoint endpoint = new LanguageEndpoint(content);` Content may be
    1. A string representing the data to be analyzed
    1. A filename containing the data to be analyzed
    1. A valid URL to a web page containing the data to be analyzed
1. Execute the call: `RosetteResponse response = endpoint.Call(api);`  The response is an object containing `Content`, which is an IDictionary, and `ContentAsJson()`, which is the JSON representation of the `Content`. `Headers` and `StatusCode` are also available.

Please refer to the examples and [API documentation](https://rosette-api.github.io/dotnet/) for further details.  Optional parameters to the api and endpoints are accessed via methods.  Required parameters are provided through the constructors.

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

From the root directory of the source tree, `dotnet build`

### Running the tests

From the root directory of the source tree, `dotnet test tests/tests.csproj`

### Running the examples against the source code

There does not seem to be a way to compile and run all of the examples without a bit of help. The easiest way to compile and run an example is to create an empty directory using `dotnet new` and update the `.csproj` file to contain:

```csharp
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.0</TargetFramework>
      <RestoreSources>$(RestoreSources);../rosette_api/bin/Debug;https://api.nuget.org/v3/index.json</RestoreSources>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="RosetteAPI.Net" Version="1.9.0" />
  </ItemGroup>

</Project>
```
The `RestoreSources` line should be updated to reference wherever the Debug (or Release) build of the packed source lives (see above).  Also note that the Version may be different from what is shown.

Steps:

1. In an empty directory, `dotnet new console -lang c#`.  This will create several files and an obj directory
1. Remove `Program.cs`
1. Edit your `.csproj` to contain the `RestoreSources` line from the above example, making sure to include the path to the `pack` output location, and the `ItemGroup` block
1. Copy an example file into the directory
1. `dotnet run ${API_KEY}` where API_KEY is your Rosette API key
1. To run a different example, delete the one in the directory, copy the new one into the directory and `dotnet run ${API_KEY}`

Developer Note:  If you update the source, you will need to do two things:

1. From the source root, `dotnet pack`
1. `dotnet nuget locals all --clear` to clear the cached package


## Status

In development.  Nothing published to NuGet, yet
