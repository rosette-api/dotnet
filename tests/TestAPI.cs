using System;
using System.Collections.Generic;
using Xunit;
using rosette_api;

namespace rosette_api.tests
{
    public class TestAPI
    {
        private static string _defaultUri = "https://api.rosette.com/rest/v1/";
        private static string _testKey = "testKey";

        private static RosetteAPI Init() {
            return new RosetteAPI(_testKey);
        }

        [Fact]
        public void TestKey() {
            RosetteAPI api = Init();
            Assert.Equal(_testKey, api.APIKey);
        }

        [Fact]
        public void TestNullKey() {
            string key = null;
            Exception ex = Assert.Throws<ArgumentNullException>(() => new RosetteAPI(key));

            Assert.Contains("The API Key cannot be null", ex.Message);
        }

        [Fact]
        public void TestURI() {
            RosetteAPI api = Init();
            Assert.Equal(_defaultUri, api.URI);

            // test alternate url as well as auto append trailing slash
            string alternateUrl = "https://stage.rosette.com/rest/v1";
            api.UseAlternateURL(alternateUrl);
            Assert.Equal(alternateUrl + "/", api.URI);
        }

        [Fact]
        public void TestConnections() {
            RosetteAPI api = Init();
            Assert.Equal(2, api.ConcurrentConnections);

            api.AssignConcurrentConnections(6);
            Assert.Equal(6, api.ConcurrentConnections);
        }

        [Fact]
        public void TestValidCustomHeader() {
            RosetteAPI api = Init();
            Exception ex = Assert.Throws<ArgumentException>(() => api.AddCustomHeader("BogusHeader", "BogusValue"));

            Assert.Contains(@"Custom header name must begin with 'X-RosetteAPI-'", ex.Message);
        }

        [Fact]
        public void TestVersion() {
            Assert.NotEmpty(RosetteAPI.Version);
        }

        [Fact]
        public void TestTimeout() {
            RosetteAPI api = Init();
            Assert.Equal(30, api.Timeout);

            api.AssignTimeout(15);
            Assert.Equal(15, api.Timeout);
        }

        [Fact]
        public void TestDebug() {
            RosetteAPI api = Init();
            Assert.False(api.Debug);

            api.SetDebug();
            Assert.True(api.Debug);
        }

        [Fact]
        public void TestDefaultBuild() {
            RosetteAPI api = Init();

            api = api.Prepare();

            Assert.Equal(_defaultUri, api.Client.BaseAddress.AbsoluteUri);
            var acceptHeader = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
            Assert.Contains(acceptHeader, api.Client.DefaultRequestHeaders.Accept);
            foreach (string encodingType in new List<string>() { "gzip", "deflate" }) {
                var encodingHeader = new System.Net.Http.Headers.StringWithQualityHeaderValue(encodingType);
                Assert.Contains(encodingHeader, api.Client.DefaultRequestHeaders.AcceptEncoding);
            }
            Assert.Equal(api.Timeout, api.Client.Timeout.Seconds);
        }

    }
}
