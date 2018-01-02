# dotnet

Rosette API Client Library for .Net

## Summary

This is intended to be a replacement for the C# binding.  Although it is written in C#, it has the following improvements:

- targets .Net Core 2 rather than Framework
- updated to handle multi-threaded operations and address some concurrency issues in the old C# binding
- all new unit tests
- removal of brittle return types, replaced with IDictionary and JSON so that returned data reflects the latest from the server

## Status

In development.  Nothing published to NuGet. Not ready for publication.
