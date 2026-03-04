using RichardSzalay.MockHttp;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using Rosette.Api.Client.Endpoints.Core;
using Rosette.Api.Client.Models;
using Rosette.Api.Client;

namespace Rosette.Api.Tests;

public class EndpointExecutorTests
{
    private readonly Dictionary<string, object> _params;
    private readonly Dictionary<string, object> _options;
    private readonly NameValueCollection _urlParameters;
    private static readonly string _defaultUri = "https://api.rosette.com/rest/v1/*";

    public EndpointExecutorTests() {
        _params = new Dictionary<string, object>();
        _options = new Dictionary<string, object>();
        _urlParameters = new NameValueCollection();
    }

    [Fact]
    public void Content_SetStringAndAddToParams_WhenAssigned() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Empty(f.Content.ToString()!);
        f.Content = "Sample Content";
        Assert.Equal("Sample Content", f.Content);
        Assert.True(_params.ContainsKey("content"));
        Assert.False(_params.ContainsKey("contenturi"));
    }

    [Fact]
    public void Endpoint_ReturnsProvidedValue_WhenInitialized() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Equal("test", f.Endpoint);
    }

    [Fact]
    public void Content_SetUriAndAddToParams_WhenAssignedUri() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Empty(f.Content.ToString()!);
        f.Content = new Uri("http://google.com");
        Assert.Equal("http://google.com/", f.Content);
        Assert.True(_params.ContainsKey("contenturi"));
        Assert.False(_params.ContainsKey("content"));
    }

    [Fact]
    public void Content_SetFileStreamAndFilename_WhenAssignedFileStream() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Empty(f.Content.ToString()!);
        var newFile = Path.GetTempFileName();
        using (FileStream fs = File.OpenRead(newFile)) {
            f.Content = fs;
            Assert.Equal(newFile, f.Filename);
            Assert.Empty(f.Content.ToString()!);
            Assert.False(_params.ContainsKey("content"));
            Assert.False(_params.ContainsKey("contenturi"));
        }
    }

    [Fact]
    public void Language_SetLanguage_WhenAssigned() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Empty(f.Language!);
        f.Language = "eng";
        Assert.Equal("eng", f.Language);
    }

    [Fact]
    public void Genre_SetGenre_WhenAssigned() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Empty(f.Genre!);
        f.Genre = "social-media";
        Assert.Equal("social-media", f.Genre);
    }

    [Fact]
    public void FileContentType_SetContentType_WhenAssigned() {
        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Assert.Equal("text/plain", f.FileContentType);
        f.FileContentType = "octet/stream";
        Assert.Equal("octet/stream", f.FileContentType);
    }

    [Fact]
    public void Parameters_SerializeCorrectly_WhenOptionsSet() {
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();

        api.AssignClient(client);

        EndpointExecutor f = new(_params, _options, _urlParameters, "test");

        _options["opt"] = true;
        Dictionary<string, object> paramTest = new();
        paramTest["content"] = "Test Content";
        paramTest["options"] = _options;

        f.Content = "Test Content";
        Response result = f.PostCall(api);
        Assert.Equal(JsonSerializer.Serialize(paramTest), JsonSerializer.Serialize(f.Parameters));
    }

    [Fact]
    public void PostCall_ReturnsOKResponse_WhenCalledWithValidContent() {
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();

        api.AssignClient(client);

        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        f.Content = "Test content";
        Response result = f.PostCall(api);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public void GetCall_ReturnsOKResponse_WhenCalled() {
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();

        api.AssignClient(client);

        EndpointExecutor f = new(_params, _options, _urlParameters, "test");
        Response result = f.GetCall(api);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    #region Async Tests

    [Fact]
    public async Task GetCallAsync_ValidRequest_ReturnsResponse()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");

        // Act
        Response result = await executor.GetCallAsync(api);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Content);
    }

    [Fact]
    public async Task GetCallAsync_WithCancellationToken_ReturnsResponse()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        using var cts = new CancellationTokenSource();

        // Act
        Response result = await executor.GetCallAsync(api, cts.Token);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task GetCallAsync_CancelledToken_ThrowsOperationCanceledException()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(async () =>
            {
                await Task.Delay(100); // Simulate delay
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"test\": \"OK\"}")
                };
            });
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        var cts = new CancellationTokenSource();
        cts.Cancel(); // Cancel immediately

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            async () => await executor.GetCallAsync(api, cts.Token));
    }

    [Fact]
    public async Task PostCallAsync_WithStringContent_ReturnsResponse()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "Test content";

        // Act
        Response result = await executor.PostCallAsync(api);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Content);
    }

    [Fact]
    public async Task PostCallAsync_WithCancellationToken_ReturnsResponse()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "Test content";
        using var cts = new CancellationTokenSource();

        // Act
        Response result = await executor.PostCallAsync(api, cts.Token);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task PostCallAsync_CancelledToken_ThrowsOperationCanceledException()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(async () =>
            {
                await Task.Delay(100);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"test\": \"OK\"}")
                };
            });
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "Test content";
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            async () => await executor.PostCallAsync(api, cts.Token));
    }

    [Fact]
    public async Task PostCallAsync_WithFileContent_SendsMultipart()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, "test file content");

        try
        {
            using (FileStream fs = File.OpenRead(tempFile))
            {
                EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
                executor.Content = fs;

                // Act
                Response result = await executor.PostCallAsync(api);

                // Assert
                Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
                Assert.Equal(tempFile, executor.Filename);
            }
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public async Task PostCallAsync_WithFileAndOptions_SendsMultipartWithRequest()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, "test file content");

        try
        {
            using (FileStream fs = File.OpenRead(tempFile))
            {
                EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
                _options["language"] = "eng";
                executor.Content = fs;

                // Act
                Response result = await executor.PostCallAsync(api);

                // Assert
                Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
                Assert.True(_options.ContainsKey("language"));
            }
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public async Task PostCallAsync_WithUrlParameters_AppendsQueryString()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api.rosette.com/rest/v1/test?output=rosette")
            .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        _urlParameters.Add("output", "rosette");
        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "Test content";

        // Act
        Response result = await executor.PostCallAsync(api);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task PostCallAsync_WithUnicodeContent_SendsUnescapedUnicode()
    {
        // Arrange
        ApiClient api = new("testkey");
        string capturedRequest = string.Empty;
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(async (request) =>
            {
                capturedRequest = await request.Content!.ReadAsStringAsync();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"test\": \"OK\"}")
                };
            });
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "北京大学 👍🏾"; // Chinese characters and emoji

        // Act
        Response result = await executor.PostCallAsync(api);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        // Verify that the request contains actual Unicode, not escape sequences
        Assert.Contains("北京大学", capturedRequest);
    }

    [Fact]
    public async Task PostCallAsync_ErrorResponse_ThrowsHttpRequestException()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.BadRequest, "application/json", "{\"message\": \"Bad Request\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "Test content";

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            async () => await executor.PostCallAsync(api));
    }

    [Fact]
    public async Task GetCallAsync_ErrorResponse_ThrowsHttpRequestException()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(HttpStatusCode.NotFound, "application/json", "{\"message\": \"Not Found\"}");
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            async () => await executor.GetCallAsync(api));
    }

    [Fact]
    public async Task PostCallAsync_TimeoutWithCancellation_ThrowsOperationCanceledException()
    {
        // Arrange
        ApiClient api = new("testkey");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_defaultUri)
            .Respond(async () =>
            {
                await Task.Delay(5000); // 5 second delay
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"test\": \"OK\"}")
                };
            });
        var client = mockHttp.ToHttpClient();
        api.AssignClient(client);

        EndpointExecutor executor = new(_params, _options, _urlParameters, "test");
        executor.Content = "Test content";
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100)); // 100ms timeout

        // Act & Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await executor.PostCallAsync(api, cts.Token));
    }

    #endregion
}
