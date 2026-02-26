using RichardSzalay.MockHttp;
using Rosette.Api.Endpoints;
using Rosette.Api.Models;
using System.Net;

namespace Rosette.Api.Tests
{
    public class EndpointBaseTests
    {
        private static readonly string _defaultUri = "https://api.rosette.com/rest/v1/*";

        [Fact]
        public void Constructor_ValidEndpoint_SetsEndpointName()
        {
            // Arrange & Act
            var endpoint = new Entities("test content");

            // Assert
            Assert.Equal("entities", endpoint.Endpoint);
        }

        [Fact]
        public void Options_InitialState_IsEmpty()
        {
            // Arrange & Act
            var endpoint = new Entities("foo");

            // Assert
            Assert.Empty(endpoint.Options);
        }

        [Fact]
        public void Params_InitialState_IsNotEmpty()
        {
            // Arrange & Act
            var endpoint = new Entities("foo");

            // Assert
            Assert.NotEmpty(endpoint.Params);
        }

        [Fact]
        public void UrlParameters_InitialState_IsEmpty()
        {
            // Arrange & Act
            var endpoint = new Entities("foo");

            // Assert
            Assert.Empty(endpoint.UrlParameters);
        }

        [Fact]
        public void SetOption_ValidOption_AddsToOptions()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act
            e.SetOption("test", "value");

            // Assert
            Assert.Equal("value", e.Options["test"]);
        }

        [Fact]
        public void SetOption_MultipleOptions_AddsAllOptions()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act
            e.SetOption("test", "value");
            e.SetOption("test2", "value2");

            // Assert
            Assert.Equal("value", e.Options["test"]);
            Assert.Equal("value2", e.Options["test2"]);
        }

        [Fact]
        public void SetOption_NullOptionName_ThrowsArgumentNullException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => e.SetOption(null!, "value"));
        }

        [Fact]
        public void SetOption_EmptyOptionName_ThrowsArgumentException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => e.SetOption(string.Empty, "value"));
        }

        [Fact]
        public void SetOption_NullOptionValue_ThrowsArgumentNullException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => e.SetOption("test", null!));
        }

        [Fact]
        public void SetOption_ReturnsInstance_ForFluentAPI()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act
            var result = e.SetOption("test", "value");

            // Assert
            Assert.Same(e, result);
        }

        [Fact]
        public void RemoveOption_ExistingOption_RemovesOption()
        {
            // Arrange
            Entities e = new Entities("foo")
                .SetOption("test", "value");

            // Act
            e.RemoveOption("test");

            // Assert
            Assert.False(e.Options.ContainsKey("test"));
        }

        [Fact]
        public void RemoveOption_NonExistingOption_DoesNotThrow()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            var exception = Record.Exception(() => e.RemoveOption("nonexistent"));
            Assert.Null(exception);
        }

        [Fact]
        public void RemoveOption_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => e.RemoveOption(null!));
        }

        [Fact]
        public void RemoveOption_ReturnsInstance_ForFluentAPI()
        {
            // Arrange
            Entities e = new Entities("foo")
                .SetOption("test", "value");

            // Act
            var result = e.RemoveOption("test");

            // Assert
            Assert.Same(e, result);
        }

        [Fact]
        public void ClearOptions_WithOptions_RemovesAllOptions()
        {
            // Arrange
            Entities e = new Entities("foo")
                .SetOption("test1", "value1")
                .SetOption("test2", "value2");

            // Act
            e.ClearOptions();

            // Assert
            Assert.Empty(e.Options);
        }

        [Fact]
        public void ClearOptions_ReturnsInstance_ForFluentAPI()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act
            var result = e.ClearOptions();

            // Assert
            Assert.Same(e, result);
        }

        [Fact]
        public void SetUrlParameter_ValidParameter_AddsToUrlParameters()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act
            e.SetUrlParameter("test", "value");

            // Assert
            Assert.Equal("value", e.UrlParameters["test"]);
        }

        [Fact]
        public void SetUrlParameter_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => e.SetUrlParameter(null!, "value"));
        }

        [Fact]
        public void SetUrlParameter_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => e.SetUrlParameter("key", null!));
        }

        [Fact]
        public void SetUrlParameter_ReturnsInstance_ForFluentAPI()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act
            var result = e.SetUrlParameter("test", "value");

            // Assert
            Assert.Same(e, result);
        }

        [Fact]
        public void RemoveUrlParameter_ExistingParameter_RemovesParameter()
        {
            // Arrange
            Entities e = new Entities("foo")
                .SetUrlParameter("test", "value");

            // Act
            e.RemoveUrlParameter("test");

            // Assert
            Assert.Empty(e.UrlParameters);
        }

        [Fact]
        public void RemoveUrlParameter_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            Entities e = new Entities("foo");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => e.RemoveUrlParameter(null!));
        }

        [Fact]
        public void RemoveUrlParameter_ReturnsInstance_ForFluentAPI()
        {
            // Arrange
            Entities e = new Entities("foo")
                .SetUrlParameter("test", "value");

            // Act
            var result = e.RemoveUrlParameter("test");

            // Assert
            Assert.Same(e, result);
        }

        [Fact]
        public void FluentAPI_ChainMultipleMethods_AllSettersReturnSameInstance()
        {
            // Arrange & Act
            var endpoint = new Entities("content")
                .SetOption("opt1", "val1")
                .SetOption("opt2", "val2")
                .SetUrlParameter("param", "value")
                .SetLanguage("eng");

            // Assert
            Assert.Equal("val1", endpoint.Options["opt1"]);
            Assert.Equal("val2", endpoint.Options["opt2"]);
            Assert.Equal("value", endpoint.UrlParameters["param"]);
            Assert.Equal("eng", endpoint.Language);
        }

        [Fact]
        public void Call_ValidEndpoint_ReturnsResponse()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");

            // Act
            Response result = endpoint.Call(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        #region Async Tests

        [Fact]
        public async Task CallAsync_ValidEndpoint_ReturnsResponse()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        [Fact]
        public async Task CallAsync_WithCancellationToken_ReturnsResponse()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");
            using var cts = new CancellationTokenSource();

            // Act
            Response result = await endpoint.CallAsync(api, cts.Token);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        [Fact]
        public async Task CallAsync_CancelledToken_ThrowsOperationCanceledException()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
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

            Entities endpoint = new Entities("test content");
            var cts = new CancellationTokenSource();
            cts.Cancel(); // Cancel immediately

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(
                async () => await endpoint.CallAsync(api, cts.Token));
        }

        [Fact]
        public async Task CallAsync_WithTimeout_ThrowsOperationCanceledException()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
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

            Entities endpoint = new Entities("test content");
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100)); // 100ms timeout

            // Act & Assert
            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                async () => await endpoint.CallAsync(api, cts.Token));
        }

        [Fact]
        public async Task CallAsync_WithOptions_SendsOptionsToServer()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content")
                .SetOption("linkEntities", true);

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)endpoint.Options["linkEntities"]);
        }

        [Fact]
        public async Task CallAsync_WithUrlParameters_AppendsQueryString()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.rosette.com/rest/v1/entities?output=rosette")
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content")
                .SetUrlParameter("output", "rosette");

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("rosette", endpoint.UrlParameters["output"]);
        }

        [Fact]
        public async Task CallAsync_ErrorResponse_ThrowsHttpRequestException()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.BadRequest, "application/json", "{\"message\": \"Bad Request\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(
                async () => await endpoint.CallAsync(api));
        }

        [Fact]
        public async Task CallAsync_NotFoundResponse_ThrowsHttpRequestException()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.NotFound, "application/json", "{\"message\": \"Not Found\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(
                async () => await endpoint.CallAsync(api));
        }

        [Fact]
        public async Task CallAsync_UnauthorizedResponse_ThrowsHttpRequestException()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.Unauthorized, "application/json", "{\"message\": \"Unauthorized\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(
                async () => await endpoint.CallAsync(api));
        }

        [Fact]
        public async Task CallAsync_WithLanguage_SendsLanguageParameter()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content")
                .SetLanguage("eng");

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("eng", endpoint.Language);
        }

        [Fact]
        public async Task CallAsync_MultipleCallsToSameEndpoint_EachReturnsResponse()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content");

            // Act
            Response result1 = await endpoint.CallAsync(api);
            Response result2 = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result1.StatusCode);
            Assert.Equal((int)HttpStatusCode.OK, result2.StatusCode);
            Assert.NotSame(result1, result2); // Different instances
        }

        [Fact]
        public async Task CallAsync_WithFluentChaining_AllSettingsApplied()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.rosette.com/rest/v1/entities?output=rosette")
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint = new Entities("test content")
                .SetLanguage("eng")
                .SetOption("linkEntities", true)
                .SetUrlParameter("output", "rosette");

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("eng", endpoint.Language);
            Assert.True((bool)endpoint.Options["linkEntities"]);
            Assert.Equal("rosette", endpoint.UrlParameters["output"]);
        }

        [Fact]
        public async Task CallAsync_ConcurrentCalls_BothComplete()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Entities endpoint1 = new Entities("content1");
            Entities endpoint2 = new Entities("content2");

            // Act
            var task1 = endpoint1.CallAsync(api);
            var task2 = endpoint2.CallAsync(api);
            var results = await Task.WhenAll(task1, task2);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, results[0].StatusCode);
            Assert.Equal((int)HttpStatusCode.OK, results[1].StatusCode);
        }

        [Fact]
        public async Task CallAsync_InfoEndpoint_UsesGetCall()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.rosette.com/rest/v1/info")
                .Respond(HttpStatusCode.OK, "application/json", "{\"name\": \"Rosette API\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Info endpoint = new Info();

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        [Fact]
        public async Task CallAsync_PingEndpoint_UsesGetCall()
        {
            // Arrange
            ApiClient api = new ApiClient("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.rosette.com/rest/v1/ping")
                .Respond(HttpStatusCode.OK, "application/json", "{\"message\": \"pong\"}");
            var client = mockHttp.ToHttpClient();
            api.AssignClient(client);

            Ping endpoint = new Ping();

            // Act
            Response result = await endpoint.CallAsync(api);

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        #endregion
    }
}