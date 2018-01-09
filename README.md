# dotnet

Rosette API Client Library for .Net

## Summary

This is intended to be a replacement for the C# binding.  Although it is written in C#, it has the following improvements:

- targets .Net Core 2 rather than Framework
- updated to handle multi-threaded operations and address some concurrency issues in the old C# binding
- all new unit tests
- removal of brittle return types, replaced with IDictionary and JSON so that returned data reflects the latest from the server

## Documentation

TODO: Coming soon

## Packing the build

Packing the build will breate a nupkg file that can be used when running the examples.  This is the same file that is published to NuGet
From the root directory of the source tree, `dotnet pack`.  Note the output directory if you want to run examples against it later.  It will be something like `rosette_api/bin/Debug`.

## Publishing

TODO: Coming soon

## Building, Testing and Examples

The easiest way to build and test the source and run the examples against it is to use `rosetteapi/docker-dotnet`

1. cd to the root of the dotnet source
1. `docker run --rm -it -e <your api key> -v $(PWD):/source rosetteapi/docker-dotnet`

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
