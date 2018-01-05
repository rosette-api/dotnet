using Xunit;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace rosette_api.tests
{
    public class TestRosetteResponse
    {
        [Fact]
        public void CheckStatusOK() {
            Dictionary<string, string> data = new Dictionary<string, string> {
                { "content", "Some sample content" },
                { "language", "eng" }
            };
            string json = JsonConvert.SerializeObject(data);

            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
            msg.Content = new StringContent(json);
            msg.Headers.Add("Test-Header", "Test Header Content");

            RosetteResponse response = new RosetteResponse(msg);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(json, response.ContentAsJson());

        }
    }
}
