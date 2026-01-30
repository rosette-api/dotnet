## Endpoint Examples
These examples are scripts that can be run independently to demonstrate the Babel Street Analytics API functionality.
Each example file demonstrates one of the capabilities of the Analytics Platform and, when run, prints its output to the console.
You can also pass an optional, alternate url parameter, for overriding the default.

Here is one way to run the examples.

#### Latest Source with Docker
- Clone the repository.
  ```
  git clone git@github.com:rosette-api/dotnet.git
  cd dotnet

  ```

- Launch a container.
  ```
  docker run -it -v $(pwd):/dotnet debian:13

  ```

- Set up the environment.
  ```
  apt update
  apt install -y wget

  wget https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
  dpkg -i packages-microsoft-prod.deb

  apt update
  apt install -y dotnet-sdk-10.0

  dotnet --version
  dotnet --list-sdks
  dotnet --list-runtimes

  ```

- Build the package from source.
  ```
  cd /dotnet
  dotnet restore rosette_api.slnx
  dotnet build /p:Configuration=Release rosette_api.slnx
  dotnet build /p:Configuration=Debug rosette_api.slnx

  ```

- _Optional:_ Run the Unit Tests.
  ```
  dotnet test ./tests/bin/Debug/net10.0/tests.dll
  dotnet test ./tests/bin/Release/net10.0/tests.dll

  ```

- Prepare a project for the example you'd like to execute.  E.g. language.cs
  ```
  cd examples
  mkdir LanguageExample
  cd LanguageExample

  dotnet new console --framework net10.0
  cp ../Language.cs ./Program.cs
  dotnet add reference ../../rosette_api/rosette_api.csproj

  ```

- Run the example against Analytics Cloud.  In this example, your Cloud API key is stored in the environment variable `$API_KEY`.
  ```
  dotnet run $API_KEY

  ```

- Or run against an alternate url.  The key, in this case, can be anything if you aren't using authorization
  ```
  dotnet run anything123 http://example.com:8181/rest/v1

  ```
