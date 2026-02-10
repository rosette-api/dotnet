using System.Net;
using System.Text.Json;

namespace Rosette.Api.Tests
{
    public class TestRosetteResponse
    {
        [Fact]
        public void CheckStatusOK() {
            Dictionary<string, string> data = new Dictionary<string, string> {
                { "content", "Some sample content" },
                { "language", "eng" }
            };
            string json = JsonSerializer.Serialize(data);

            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
            msg.Content = new StringContent(json);
            msg.Headers.Add("Test-Header", "Test Header Content");

            Response response = new Response(msg);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(json, response.ContentAsJson());

        }
    }
}
