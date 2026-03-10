using Rosette.Api.Client.Models;
using System.Net;
using System.Text.Json;

namespace Rosette.Api.Tests;

public class ResponseTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsStatusCodeAndContent_WhenHttpResponseIsOK() {
        Dictionary<string, string> data = new()
        {
            { "content", "Some sample content" },
            { "language", "eng" }
        };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK);
        msg.Content = new StringContent(json);
        msg.Headers.Add("Test-Header", "Test Header Content");

        Response response = new(msg);

        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(json, response.ContentAsJson());

    }

    [Fact]
    public void ContentAsJson_WithPrettyTrue_ReturnsFormattedJson()
    {
        // Arrange
        Dictionary<string, string> data = new()
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK);
        msg.Content = new StringContent(json);

        Response response = new(msg);

        // Act
        string prettyJson = (string)response.ContentAsJson(pretty: true);

        // Assert
        Assert.Contains("\n", prettyJson); // Pretty JSON should have newlines
        Assert.Contains("key1", prettyJson);
        Assert.Contains("value1", prettyJson);
    }

    [Fact]
    public void ContentAsJson_WithPrettyFalse_ReturnsCompactJson()
    {
        // Arrange
        Dictionary<string, string> data = new()
        {
            { "key1", "value1" }
        };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK);
        msg.Content = new StringContent(json);

        Response response = new(msg);

        // Act
        string compactJson = (string)response.ContentAsJson(pretty: false);

        // Assert
        Assert.NotNull(compactJson);
        Assert.Contains("key1", compactJson);
    }

    #endregion

    #region Error Response Tests

    [Fact]
    public void Constructor_ThrowsHttpRequestException_WhenStatusCodeIsNotSuccess()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.BadRequest)
        {
            ReasonPhrase = "Bad Request",
            Content = new StringContent("Invalid request parameters")
        };

        // Act & Assert
        var exception = Assert.Throws<HttpRequestException>(() => new Response(msg));
        Assert.Contains("400", exception.Message);
        Assert.Contains("Bad Request", exception.Message);
        Assert.Contains("Invalid request parameters", exception.Message);
    }

    [Fact]
    public void Constructor_ThrowsHttpRequestException_WhenStatusCodeIs404()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.NotFound)
        {
            ReasonPhrase = "Not Found",
            Content = new StringContent("Resource not found")
        };

        // Act & Assert
        var exception = Assert.Throws<HttpRequestException>(() => new Response(msg));
        Assert.Contains("404", exception.Message);
        Assert.Contains("Not Found", exception.Message);
    }

    [Fact]
    public void Constructor_ThrowsHttpRequestException_WhenStatusCodeIs500()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.InternalServerError)
        {
            ReasonPhrase = "Internal Server Error",
            Content = new StringContent("Server error occurred")
        };

        // Act & Assert
        var exception = Assert.Throws<HttpRequestException>(() => new Response(msg));
        Assert.Contains("500", exception.Message);
        Assert.Contains("Internal Server Error", exception.Message);
    }

    [Fact]
    public void Constructor_ThrowsHttpRequestException_WhenStatusCodeIs401()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.Unauthorized)
        {
            ReasonPhrase = "Unauthorized",
            Content = new StringContent("Invalid API key")
        };

        // Act & Assert
        var exception = Assert.Throws<HttpRequestException>(() => new Response(msg));
        Assert.Contains("401", exception.Message);
        Assert.Contains("Unauthorized", exception.Message);
        Assert.Contains("Invalid API key", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_ThrowsHttpRequestException_WhenStatusCodeIsNotSuccess()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.BadRequest)
        {
            ReasonPhrase = "Bad Request",
            Content = new StringContent("Invalid parameters")
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            async () => await Response.CreateAsync(msg));
        Assert.Contains("400", exception.Message);
        Assert.Contains("Bad Request", exception.Message);
        Assert.Contains("Invalid parameters", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_ThrowsHttpRequestException_WhenStatusCodeIs503()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.ServiceUnavailable)
        {
            ReasonPhrase = "Service Unavailable",
            Content = new StringContent("Service temporarily unavailable")
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            async () => await Response.CreateAsync(msg));
        Assert.Contains("503", exception.Message);
        Assert.Contains("Service Unavailable", exception.Message);
    }

    #endregion

    #region Gzip Decompression Tests

    [Fact]
    public void Constructor_DecompressesGzipContent_WhenContentIsGzipped()
    {
        // Arrange
        Dictionary<string, string> data = new()
        {
            { "result", "success" },
            { "message", "Data decompressed successfully" }
        };
        string json = JsonSerializer.Serialize(data);

        // Create gzipped content
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
        byte[] gzipBytes;
        using (var outputStream = new MemoryStream())
        {
            using (var gzipStream = new System.IO.Compression.GZipStream(outputStream, System.IO.Compression.CompressionMode.Compress))
            {
                gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
            }
            gzipBytes = outputStream.ToArray();
        }

        // Verify gzip header
        Assert.Equal(0x1f, gzipBytes[0]);
        Assert.Equal(0x8b, gzipBytes[1]);
        Assert.Equal(0x08, gzipBytes[2]);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(gzipBytes)
        };

        // Act
        Response response = new(msg);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("result", response.Content.Keys);
        Assert.Equal("success", response.Content["result"].ToString());
    }

    [Fact]
    public async Task CreateAsync_DecompressesGzipContent_WhenContentIsGzipped()
    {
        // Arrange
        Dictionary<string, string> data = new()
        {
            { "status", "ok" },
            { "compressed", "true" }
        };
        string json = JsonSerializer.Serialize(data);

        // Create gzipped content
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
        byte[] gzipBytes;
        using (var outputStream = new MemoryStream())
        {
            using (var gzipStream = new System.IO.Compression.GZipStream(outputStream, System.IO.Compression.CompressionMode.Compress))
            {
                gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
            }
            gzipBytes = outputStream.ToArray();
        }

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(gzipBytes)
        };

        // Act
        Response response = await Response.CreateAsync(msg);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("status", response.Content.Keys);
        Assert.Equal("ok", response.Content["status"].ToString());
        Assert.Equal("true", response.Content["compressed"].ToString());
    }

    [Fact]
    public void Constructor_HandlesLargeGzipContent_WhenContentIsLarge()
    {
        // Arrange - Create a large JSON object
        var largeData = new Dictionary<string, string>();
        for (int i = 0; i < 1000; i++)
        {
            largeData.Add($"key{i}", $"value{i}_{new string('x', 100)}");
        }
        string json = JsonSerializer.Serialize(largeData);

        // Create gzipped content
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
        byte[] gzipBytes;
        using (var outputStream = new MemoryStream())
        {
            using (var gzipStream = new System.IO.Compression.GZipStream(outputStream, System.IO.Compression.CompressionMode.Compress))
            {
                gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
            }
            gzipBytes = outputStream.ToArray();
        }

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(gzipBytes)
        };

        // Act
        Response response = new(msg);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(1000, response.Content.Count);
        Assert.Contains("key500", response.Content.Keys);
    }

    [Fact]
    public void Constructor_HandlesUncompressedContent_WhenContentIsNotGzipped()
    {
        // Arrange
        Dictionary<string, string> data = new()
        {
            { "compressed", "false" }
        };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };

        // Act
        Response response = new(msg);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("compressed", response.Content.Keys);
        Assert.Equal("false", response.Content["compressed"].ToString());
    }

    #endregion

    #region Internal Method Tests via Reflection

    [Fact]
    public void ContentToString_ReturnsEmptyString_WhenHttpContentIsNull()
    {
        // Act - Use reflection to access internal method
        var methodInfo = typeof(Response).GetMethod("ContentToString", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.NotNull(methodInfo);

        string result = (string)methodInfo.Invoke(null, new object?[] { null })!;

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task ContentToStringAsync_ReturnsEmptyString_WhenHttpContentIsNull()
    {
        // Act - Use reflection to access internal method
        var methodInfo = typeof(Response).GetMethod("ContentToStringAsync", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.NotNull(methodInfo);

        var task = (Task<string>)methodInfo.Invoke(null, new object?[] { null, CancellationToken.None })!;
        string result = await task;

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task ContentToStringAsync_ReturnsContent_WhenHttpContentIsNotNull()
    {
        // Arrange
        var content = new StringContent("Test content");

        // Act - Use reflection to access internal method
        var methodInfo = typeof(Response).GetMethod("ContentToStringAsync", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.NotNull(methodInfo);

        var task = (Task<string>)methodInfo.Invoke(null, new object?[] { content, CancellationToken.None })!;
        string result = await task;

        // Assert
        Assert.Equal("Test content", result);
    }

    [Fact]
    public async Task ContentToStringAsync_WithCancellationToken_ReturnsContent()
    {
        // Arrange
        var content = new StringContent("Test with cancellation token");
        var cts = new CancellationTokenSource();

        // Act - Use reflection to access internal method
        var methodInfo = typeof(Response).GetMethod("ContentToStringAsync", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.NotNull(methodInfo);

        var task = (Task<string>)methodInfo.Invoke(null, new object?[] { content, cts.Token })!;
        string result = await task;

        // Assert
        Assert.Equal("Test with cancellation token", result);
    }

    #endregion

    #region Headers Tests

    [Fact]
    public void Constructor_ProcessesResponseHeaders_WhenHeadersExist()
    {
        // Arrange
        Dictionary<string, string> data = new() { { "test", "data" } };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };
        msg.Headers.Add("X-Custom-Header", "CustomValue");
        msg.Headers.Add("X-Request-Id", "12345");

        // Act
        Response response = new(msg);

        // Assert
        Assert.Contains("X-Custom-Header", response.Headers.Keys);
        Assert.Equal("CustomValue", response.Headers["X-Custom-Header"]);
        // Note: Header names might be normalized (X-Request-Id becomes X-Request-ID)
        Assert.True(response.Headers.ContainsKey("X-Request-Id") || response.Headers.ContainsKey("X-Request-ID"));
    }

    [Fact]
    public void Constructor_ProcessesContentHeaders_WhenContentHeadersExist()
    {
        // Arrange
        Dictionary<string, string> data = new() { { "test", "data" } };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        // Act
        Response response = new(msg);

        // Assert
        Assert.Contains("Content-Type", response.Headers.Keys);
        Assert.Contains("application/json", response.Headers["Content-Type"]);
    }

    [Fact]
    public async Task CreateAsync_ProcessesHeaders_WhenHeadersExist()
    {
        // Arrange
        Dictionary<string, string> data = new() { { "async", "test" } };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };
        msg.Headers.Add("X-Async-Header", "AsyncValue");

        // Act
        Response response = await Response.CreateAsync(msg);

        // Assert
        Assert.Contains("X-Async-Header", response.Headers.Keys);
        Assert.Equal("AsyncValue", response.Headers["X-Async-Header"]);
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void Constructor_HandlesEmptyContent_WhenContentIsEmpty()
    {
        // Arrange
        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent("{}")
        };

        // Act
        Response response = new(msg);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Empty(response.Content);
    }

    [Fact]
    public void Constructor_HandlesMultipleHeaders_WhenManyHeadersExist()
    {
        // Arrange
        Dictionary<string, string> data = new() { { "data", "test" } };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };
        msg.Headers.Add("Header1", "Value1");
        msg.Headers.Add("Header2", "Value2");
        msg.Headers.Add("Header3", "Value3");

        // Act
        Response response = new(msg);

        // Assert
        Assert.True(response.Headers.Count >= 3);
        Assert.Equal("Value1", response.Headers["Header1"]);
        Assert.Equal("Value2", response.Headers["Header2"]);
        Assert.Equal("Value3", response.Headers["Header3"]);
    }

    [Fact]
    public void Constructor_HandlesComplexJsonContent_WhenContentIsNested()
    {
        // Arrange
        var complexData = new
        {
            user = new { id = 1, name = "John" },
            metadata = new { created = "2026-03-10", version = "1.0" },
            items = new[] { "item1", "item2", "item3" }
        };
        string json = JsonSerializer.Serialize(complexData);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };

        // Act
        Response response = new(msg);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("user", response.Content.Keys);
        Assert.Contains("metadata", response.Content.Keys);
        Assert.Contains("items", response.Content.Keys);
    }

    [Fact]
    public async Task CreateAsync_WithCancellationToken_CompletesSuccessfully()
    {
        // Arrange
        Dictionary<string, string> data = new() { { "status", "success" } };
        string json = JsonSerializer.Serialize(data);

        HttpResponseMessage msg = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };
        var cts = new CancellationTokenSource();

        // Act
        Response response = await Response.CreateAsync(msg, cts.Token);

        // Assert
        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("status", response.Content.Keys);
    }

    #endregion
}
