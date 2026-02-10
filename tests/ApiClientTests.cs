namespace Rosette.Api.Tests
{
    public class ApiClientTests
    {
        private static readonly string _defaultUri = "https://api.rosette.com/rest/v1/";
        private static readonly string _testKey = "testKey";

        private static ApiClient Init() {
            return new ApiClient(_testKey);
        }

        [Fact]
        public void TestKey() {
            ApiClient api = Init();
            Assert.Equal(_testKey, api.APIKey);
        }

        [Fact]
        public void TestNullKey() {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string key = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
            Exception ex = Assert.Throws<ArgumentNullException>(() => new ApiClient(key));
#pragma warning restore CS8604 // Possible null reference argument.

            Assert.Equal("Value cannot be null. (Parameter 'apiKey')", ex.Message);
        }

        [Fact]
        public void TestURI() {
            ApiClient api = Init();
            Assert.Equal(_defaultUri, api.URI);

            // test alternate url as well as auto append trailing slash
            string alternateUrl = "https://stage.rosette.com/rest/v1";
            api.UseAlternateURL(alternateUrl);
            Assert.Equal(alternateUrl + "/", api.URI);
        }

        [Fact]
        public void TestConnections() {
            ApiClient api = Init();
            Assert.Equal(2, api.ConcurrentConnections);

            api.AssignConcurrentConnections(6);
            Assert.Equal(6, api.ConcurrentConnections);
        }

        [Fact]
        public void TestValidCustomHeader() {
            ApiClient api = Init();
            Exception ex = Assert.Throws<ArgumentException>(() => api.AddCustomHeader("BogusHeader", "BogusValue"));

            Assert.Contains(@"Custom header name must begin with 'X-RosetteAPI-'", ex.Message);
        }

        [Fact]
        public void TestVersion() {
            Assert.NotEmpty(ApiClient.Version);
        }

        [Fact]
        public void TestTimeout() {
            ApiClient api = Init();
            Assert.Equal(300, api.Timeout);

            api.AssignTimeout(15);
            Assert.Equal(15, api.Timeout);
        }

        [Fact]
        public void TestDebug() {
            ApiClient api = Init();
            Assert.False(api.Debug);

            api.SetDebug();
            Assert.True(api.Debug);
        }

        [Fact]
        public void TestDefaultClient() {
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

    }
}
