using Xunit;
using RichardSzalay.MockHttp;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using rosette_api;

namespace tests
{
    public class TestEndpointFunctions
    {
        private readonly Dictionary<string, object> _params;
        private readonly Dictionary<string, object> _options;
        private readonly NameValueCollection _urlParameters;
        private static readonly string _defaultUri = "https://api.rosette.com/rest/v1/*";

        public TestEndpointFunctions() {
            _params = new Dictionary<string, object>();
            _options = new Dictionary<string, object>();
            _urlParameters = new NameValueCollection();
        }

        [Fact]
        public void CheckContent() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Content.ToString()!);
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
            Assert.Empty(f.Content.ToString()!);
            f.Content = new Uri("http://google.com");
            Assert.Equal("http://google.com/", f.Content);
            Assert.True(_params.ContainsKey("contenturi"));
            Assert.False(_params.ContainsKey("content"));
        }

        [Fact]
        public void CheckFilename() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
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
        public void CheckLanguage() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Language!);
            f.Language = "eng";
            Assert.Equal("eng", f.Language);
        }

        [Fact]
        public void CheckGenre() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Empty(f.Genre!);
            f.Genre = "social-media";
            Assert.Equal("social-media", f.Genre);
        }

        [Fact]
        public void CheckFileContentType() {
            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            Assert.Equal("text/plain", f.FileContentType);
            f.FileContentType = "octet/stream";
            Assert.Equal("octet/stream", f.FileContentType);
        }

        [Fact]
        public void TestParameterSerialization() {
            RosetteAPI api = new RosetteAPI("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();

            api.AssignClient(client);

            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");

            _options["opt"] = true;
            Dictionary<string, object> paramTest = new Dictionary<string, object>();
            paramTest["content"] = "Test Content";
            paramTest["options"] = _options;

            f.Content = "Test Content";
            RosetteResponse result = f.PostCall(api);
            Assert.Equal(JsonSerializer.Serialize(paramTest), JsonSerializer.Serialize(f.Parameters));
        }

        [Fact]
        public void TestPostCall() {
            RosetteAPI api = new RosetteAPI("testkey");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(_defaultUri)
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
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
                .Respond(HttpStatusCode.OK, "application/json", "{\"test\": \"OK\"}");
            var client = mockHttp.ToHttpClient();

            api.AssignClient(client);

            EndpointFunctions f = new EndpointFunctions(_params, _options, _urlParameters, "test");
            RosetteResponse result = f.GetCall(api);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
