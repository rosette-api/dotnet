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

    #endregion
}
