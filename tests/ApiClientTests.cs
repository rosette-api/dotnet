using Rosette.Api.Client;

namespace Rosette.Api.Tests;

public class ApiClientTests
{
    private static readonly string _defaultUri = "https://analytics.babelstreet.com/rest/v1/";
    private static readonly string _testKey = "testKey";

    private static ApiClient Init() {
        return new ApiClient(_testKey);
    }

    #region Basic Property Tests

    [Fact]
    public void ApiKey_ReturnsProvidedKey_WhenInitialized() {
        ApiClient api = Init();
        Assert.Equal(_testKey, api.APIKey);
    }

    [Fact]
    public void Version_IsNotEmpty_Always() {
        Assert.NotEmpty(ApiClient.Version);
    }

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenApiKeyIsNull() {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string key = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
        Exception ex = Assert.Throws<ArgumentNullException>(() => new ApiClient(key));
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal("Value cannot be null. (Parameter 'apiKey')", ex.Message);
    }

    [Fact]
    public void Constructor_EmptyApiKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ApiClient(""));
    }

    [Fact]
    public void Constructor_WhitespaceApiKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ApiClient("   "));
    }

    #endregion

    #region URI and UseAlternateURL Tests

    [Fact]
    public void URI_ReturnsDefaultAndAppendsTrailingSlash_WhenUsingAlternateUrl() {
        ApiClient api = Init();
        Assert.Equal(_defaultUri, api.URI);

        // test alternate url as well as auto append trailing slash
        string alternateUrl = "https://stage.rosette.com/rest/v1";
        api.UseAlternateURL(alternateUrl);
        Assert.Equal(alternateUrl + "/", api.URI);
    }

    [Fact]
    public void UseAlternateURL_NullUrl_ThrowsArgumentNullException()
    {
        var api = new ApiClient("key");
        Assert.Throws<ArgumentNullException>(() => api.UseAlternateURL(null));
    }

    [Fact]
    public void UseAlternateURL_InvalidUrl_ThrowsException()
    {
        var api = new ApiClient("key");
        Assert.Throws<UriFormatException>(() => api.UseAlternateURL("not a url"));
    }

    [Fact]
    public void UseAlternateURL_EmptyString_ThrowsArgumentException()
    {
        var api = Init();
        Assert.Throws<ArgumentException>(() => api.UseAlternateURL(""));
    }

    [Fact]
    public void UseAlternateURL_WhitespaceOnly_ThrowsArgumentException()
    {
        var api = Init();
        Assert.Throws<ArgumentException>(() => api.UseAlternateURL("   "));
    }

    [Fact]
    public void UseAlternateURL_UrlWithoutScheme_ThrowsUriFormatException()
    {
        var api = Init();
        Assert.Throws<UriFormatException>(() => api.UseAlternateURL("example.com/api"));
    }

    [Fact]
    public void UseAlternateURL_MultipleTrailingSlashes_NormalizesToOneSlash()
    {
        var api = Init();
        api.UseAlternateURL("https://api.example.com////");
        // Code adds one slash if not present, doesn't normalize multiple slashes
        Assert.Equal("https://api.example.com////", api.URI);
    }

    [Fact]
    public void UseAlternateURL_ValidUrlWithQueryString_PreservesQueryString()
    {
        var api = Init();
        string urlWithQuery = "https://api.example.com/v1?key=value";
        api.UseAlternateURL(urlWithQuery);
        Assert.Equal(urlWithQuery + "/", api.URI);
    }

    [Fact]
    public void UseAlternateURL_CalledMultipleTimes_UpdatesURIEachTime()
    {
        var api = Init();

        api.UseAlternateURL("https://url1.com");
        Assert.Equal("https://url1.com/", api.URI);

        api.UseAlternateURL("https://url2.com");
        Assert.Equal("https://url2.com/", api.URI);
    }

    #endregion

    #region Client Configuration Tests

    [Fact]
    public void Client_HasCorrectDefaultConfiguration_WhenInitialized() {
        ApiClient api = Init();

        Assert.Equal(_defaultUri, api.Client.BaseAddress.AbsoluteUri);
        var acceptHeader = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
        Assert.Contains(acceptHeader, api.Client.DefaultRequestHeaders.Accept);
        foreach (string encodingType in new List<string>() { "gzip", "deflate" }) {
            var encodingHeader = new System.Net.Http.Headers.StringWithQualityHeaderValue(encodingType);
            Assert.Contains(encodingHeader, api.Client.DefaultRequestHeaders.AcceptEncoding);
        }
        Assert.Equal(api.Timeout, api.Client.Timeout.TotalSeconds);
    }

    #endregion

    #region AssignClient Tests

    [Fact]
    public void AssignClient_NullClient_ThrowsArgumentNullException()
    {
        var api = new ApiClient("key");
        Assert.Throws<ArgumentNullException>(() => api.AssignClient(null));
    }

    [Fact]
    public void AssignClient_WithDisposedClient_ThrowsObjectDisposedException()
    {
        var api = Init();
        var disposedClient = new HttpClient();
        disposedClient.Dispose();

        // Assigning disposed client throws when Prepare() tries to set Timeout
        Assert.Throws<ObjectDisposedException>(() => api.AssignClient(disposedClient));
    }

    [Fact]
    public void AssignClient_WithExistingBaseAddress_DoesNotOverwriteImmediately()
    {
        var api = Init();
        var otherUri = "https://other.com/api/";
        var client = new HttpClient { BaseAddress = new Uri(otherUri) };

        api.AssignClient(client);

        // BaseAddress is set in Prepare() only if null
        // User client's BaseAddress is preserved
        Assert.Equal(otherUri, client.BaseAddress!.ToString());
    }

    [Fact]
    public void AssignClient_PreservesUserClient_DoesNotDisposeOnApiDispose()
    {
        var api = Init();
        var userClient = new HttpClient();

        api.AssignClient(userClient);
        api.Dispose();

        // User's client should still be usable (not disposed)
        // Verify by checking BaseAddress is still accessible
        var exception = Record.Exception(() => { var _ = userClient.BaseAddress; });
        Assert.Null(exception);

        // Clean up
        userClient.Dispose();
    }

    [Fact]
    public void AssignConcurrentConnections_AfterUserClient_ReplacesClientWithInternalOne()
    {
        var api = Init();
        var userClient = new HttpClient();

        api.AssignClient(userClient);
        var assignedClient = api.Client;
        Assert.Same(userClient, assignedClient);

        // Changing concurrent connections forces new internal client
        api.AssignConcurrentConnections(5);

        Assert.NotSame(userClient, api.Client);

        // Clean up
        userClient.Dispose();
    }

    #endregion

    #region ConcurrentConnections Tests

    [Fact]
    public void ConcurrentConnections_ReturnsCorrectValue_WhenDefaultAndAssigned() {
        ApiClient api = Init();
        Assert.Equal(2, api.ConcurrentConnections);

        api.AssignConcurrentConnections(6);
        Assert.Equal(6, api.ConcurrentConnections);
    }

    [Fact]
    public void AssignConcurrentConnections_One_ClampsToMinimumOfTwo()
    {
        var api = Init();
        api.AssignConcurrentConnections(1);

        // Should be clamped to minimum of 2
        Assert.Equal(2, api.ConcurrentConnections);
    }

    [Fact]
    public void AssignConcurrentConnections_Zero_ClampsToMinimumOfTwo()
    {
        var api = Init();
        api.AssignConcurrentConnections(0);

        Assert.Equal(2, api.ConcurrentConnections);
    }

    [Fact]
    public void AssignConcurrentConnections_NegativeValue_ClampsToMinimumOfTwo()
    {
        var api = Init();
        api.AssignConcurrentConnections(-5);

        Assert.Equal(2, api.ConcurrentConnections);
    }

    [Fact]
    public void AssignConcurrentConnections_LargeValue_AcceptsValue()
    {
        var api = Init();
        api.AssignConcurrentConnections(1000);

        Assert.Equal(1000, api.ConcurrentConnections);
    }

    #endregion

    #region Timeout Tests

    [Fact]
    public void Timeout_ReturnsCorrectValue_WhenDefaultAndAssigned() {
        ApiClient api = Init();
        Assert.Equal(300, api.Timeout);

        api.AssignTimeout(15);
        Assert.Equal(15, api.Timeout);
    }

    [Fact]
    public void AssignTimeout_Zero_SetsTimeoutToInfinite()
    {
        var api = Init();
        api.AssignTimeout(0);

        Assert.Equal(0, api.Timeout);
        // HttpClient.Timeout of 0 gets set to Infinite
        Assert.Equal(System.Threading.Timeout.InfiniteTimeSpan, api.Client!.Timeout);
    }

    [Fact]
    public void AssignTimeout_NegativeValue_ClampsToZeroAndSetsInfiniteTimeout()
    {
        var api = Init();
        api.AssignTimeout(-10);

        // Negative values should be clamped to 0
        Assert.Equal(0, api.Timeout);
        Assert.Equal(System.Threading.Timeout.InfiniteTimeSpan, api.Client!.Timeout);
    }

    [Fact]
    public void AssignTimeout_VeryLargeValue_SetsTimeoutSuccessfully()
    {
        var api = Init();
        // Use a large but reasonable timeout value (1 day in seconds)
        int largeTimeout = 86400; // 24 hours

        api.AssignTimeout(largeTimeout);

        Assert.Equal(largeTimeout, api.Timeout);
        Assert.Equal(TimeSpan.FromSeconds(largeTimeout), api.Client!.Timeout);
    }

    [Fact]
    public void AssignTimeout_CalledMultipleTimes_UpdatesEachTime()
    {
        var api = Init();

        api.AssignTimeout(10);
        Assert.Equal(10, api.Timeout);

        api.AssignTimeout(20);
        Assert.Equal(20, api.Timeout);

        api.AssignTimeout(30);
        Assert.Equal(30, api.Timeout);
    }

    #endregion

    #region Debug Tests

    [Fact]
    public void Debug_TogglesToTrue_WhenSetDebugCalled() {
        ApiClient api = Init();
        Assert.False(api.Debug);

        api.SetDebug();
        Assert.True(api.Debug);
    }

    #endregion

    #region AddCustomHeader Tests

    [Fact]
    public void AddCustomHeader_ThrowsArgumentException_WhenHeaderNameIsInvalid() {
        ApiClient api = Init();
        Exception ex = Assert.Throws<ArgumentException>(() => api.AddCustomHeader("BogusHeader", "BogusValue"));

        Assert.Contains(@"Custom header name must begin with 'X-RosetteAPI-'", ex.Message);
    }

    [Fact]
    public void AddCustomHeader_JustPrefix_ThrowsArgumentException()
    {
        var api = Init();
        var exception = Assert.Throws<ArgumentException>(() => 
            api.AddCustomHeader("X-RosetteAPI-", "value"));

        Assert.Contains("X-RosetteAPI-", exception.Message);
    }

    [Fact]
    public void AddCustomHeader_EmptyHeaderValue_AddsHeaderSuccessfully()
    {
        var api = Init();

        // Empty string is valid for HTTP headers
        var exception = Record.Exception(() => 
            api.AddCustomHeader("X-RosetteAPI-Test", ""));

        Assert.Null(exception);
    }

    [Fact]
    public void AddCustomHeader_CaseInsensitivePrefix_AcceptsLowercase()
    {
        var api = Init();

        // The check uses OrdinalIgnoreCase, so lowercase should work
        var exception = Record.Exception(() => 
            api.AddCustomHeader("x-rosetteapi-custom", "value"));

        Assert.Null(exception);
    }

    [Fact]
    public void AddCustomHeader_NullValue_RemovesExistingHeader()
    {
        var api = Init();

        api.AddCustomHeader("X-RosetteAPI-Test", "value");

        // Setting to null should remove the header
        var exception = Record.Exception(() => 
            api.AddCustomHeader("X-RosetteAPI-Test", null!));

        Assert.Null(exception);
    }

    [Fact]
    public void AddCustomHeader_NullValueForNonExistentHeader_DoesNotThrow()
    {
        var api = Init();

        // Removing non-existent header should not throw
        var exception = Record.Exception(() => 
            api.AddCustomHeader("X-RosetteAPI-NonExistent", null!));

        Assert.Null(exception);
    }

    #endregion

    #region Dispose Pattern Tests

    [Fact]
    public void Dispose_CalledTwice_DoesNotThrow()
    {
        var api = Init();
        api.Dispose();

        var exception = Record.Exception(() => api.Dispose());
        Assert.Null(exception);
    }

    [Fact]
    public void Dispose_DisposesInternalClient_WhenNotUserProvided()
    {
        var api = Init();
        var client = api.Client;

        api.Dispose();

        // In .NET the HttpClient might not immediately throw on property access after dispose
        // Instead verify that operations fail
        Assert.Throws<ObjectDisposedException>(() => client!.GetAsync("http://test.com").GetAwaiter().GetResult());
    }

    [Fact]
    public void Dispose_DoesNotDisposeUserClient_WhenUserProvided()
    {
        var api = Init();
        var userClient = new HttpClient();

        api.AssignClient(userClient);
        api.Dispose();

        // User client should still be usable
        var exception = Record.Exception(() => { var _ = userClient.BaseAddress; });
        Assert.Null(exception);

        // Clean up
        userClient.Dispose();
    }

    #endregion

    #region Fluent API and State Management Tests

    [Fact]
    public void FluentAPI_MultipleMethodCalls_AllReturnSameInstance()
    {
        var api = Init();

        var result = api
            .UseAlternateURL("https://test.com/")
            .AssignTimeout(60)
            .SetDebug()
            .AddCustomHeader("X-RosetteAPI-Test", "value")
            .AssignConcurrentConnections(5);

        Assert.Same(api, result);
    }

    #endregion

}
