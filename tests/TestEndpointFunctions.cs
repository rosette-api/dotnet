using Xunit;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace rosette_api.tests
{
    public class TestEndpointFunctions
    {
        private Dictionary<string, object> _params;
        private Dictionary<string, object> _options;
        private NameValueCollection _urlParameters;
        private static string _defaultUri = "https://api.rosette.com/rest/v1/*";

        public TestEndpointFunctions() {
            _params = new Dictionary<string, object>();
            _options = new Dictionary<string, object>();
            _urlParameters = new NameValueCollection();
        }
        [Fact]
        public void CheckContent() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Content);
            f.Content = "Sample Content";
            Assert.Equal("Sample Content", f.Content);
            Assert.True(_params.ContainsKey("content"));
            Assert.False(_params.ContainsKey("contenturi"));

        }

        [Fact]
        public void CheckEndpoint() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Equal("test", f.Endpoint);
        }

        [Fact]
        public void CheckContentUri() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Content);
            f.Content = "http://google.com";
            Assert.Equal("http://google.com", f.Content);
            Assert.True(_params.ContainsKey("contenturi"));
            Assert.False(_params.ContainsKey("content"));
        }

        [Fact]
        public void CheckFilename() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Content);
            var newFile = Path.GetTempFileName();
            f.Content = newFile;

            Assert.Equal(newFile, f.Filename);
            Assert.Equal(string.Empty, f.Content);
            Assert.False(_params.ContainsKey("content"));
            Assert.False(_params.ContainsKey("contenturi"));
        }

        [Fact]
        public void CheckLanguage() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Language);
            f.Language = "eng";
            Assert.Equal("eng", f.Language);
        }

        [Fact]
        public void CheckGenre() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Genre);
            f.Genre = "social-media";
            Assert.Equal("social-media", f.Genre);
        }

        [Fact]
        public void TestPostCall() {
            RosetteAPI api = new RosetteAPI("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{'test': 'OK'}");
            var client = mockHttp.ToHttpClient();

            api.AssignClient(client);

            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            f.Content = "Test content";
            RosetteResponse result = f.PostCall(api);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void TestGetCall() {
            RosetteAPI api = new RosetteAPI("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{'test': 'OK'}");
            var client = mockHttp.ToHttpClient();

            api.AssignClient(client);

            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            RosetteResponse result = f.GetCall(api);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
