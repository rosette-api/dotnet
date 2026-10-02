using System.Net;
using System.Text.Json;
using RichardSzalay.MockHttp;
using Rosette.Api.Client;
using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class RecordSimilarityTests
{
    private static RecordSimilarity CreateEndpoint() => new(
        new Dictionary<string, RecordSimilarityFieldInfo>(),
        new RecordSimilarityProperties(),
        new RecordSimilarityRecords([], []));

    private static void AssertRequestMethod(RecordSimilarity endpoint, string wireValue)
    {
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(endpoint.Params));
        Assert.Equal(wireValue, document.RootElement.GetProperty("comparisonMethod").GetString());
        Assert.False(document.RootElement.GetProperty("properties").TryGetProperty("comparisonMethod", out _));
        Assert.True(document.RootElement.TryGetProperty("fields", out _));
        Assert.True(document.RootElement.TryGetProperty("records", out _));
    }

    [Fact]
    public void Constructor_DefaultsToOneToOne()
    {
        var endpoint = CreateEndpoint();
        Assert.Equal(RecordSimilarityComparisonMethod.ONE_TO_ONE, endpoint.ComparisonMethod);
        AssertRequestMethod(endpoint, "one_to_one");
    }

    [Theory]
    [MemberData(nameof(ComparisonMethods), MemberType = typeof(RecordSimilarityTestData))]
    public void Constructor_SetsComparisonMethod(RecordSimilarityComparisonMethod method, string wireValue)
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords([], []);
        var endpoint = new RecordSimilarity(fields, properties, records, method);

        Assert.Equal(method, endpoint.ComparisonMethod);
        Assert.Same(fields, endpoint.Params["fields"]);
        Assert.Same(properties, endpoint.Params["properties"]);
        Assert.Same(records, endpoint.Params["records"]);
        AssertRequestMethod(endpoint, wireValue);
    }

    [Theory]
    [MemberData(nameof(ComparisonMethods), MemberType = typeof(RecordSimilarityTestData))]
    public void SetComparisonMethod_UpdatesRequestAndReturnsThis(RecordSimilarityComparisonMethod method, string wireValue)
    {
        var endpoint = CreateEndpoint();
        Assert.Same(endpoint, endpoint.SetComparisonMethod(method));
        Assert.Equal(method, endpoint.ComparisonMethod);
        AssertRequestMethod(endpoint, wireValue);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void SetComparisonMethod_RejectsUndefinedValues(int value)
    {
        var endpoint = CreateEndpoint();
        Assert.Throws<ArgumentOutOfRangeException>(() => endpoint.SetComparisonMethod((RecordSimilarityComparisonMethod)value));
        Assert.Equal(RecordSimilarityComparisonMethod.ONE_TO_ONE, endpoint.ComparisonMethod);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void Constructor_RejectsUndefinedComparisonMethods(int value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new RecordSimilarity(
            new Dictionary<string, RecordSimilarityFieldInfo>(),
            new RecordSimilarityProperties(),
            new RecordSimilarityRecords([], []),
            (RecordSimilarityComparisonMethod)value));
    }

    [Theory]
    [MemberData(nameof(ComparisonMethods), MemberType = typeof(RecordSimilarityTestData))]
    public async Task CallAsync_SendsComparisonMethodInRequestBody(RecordSimilarityComparisonMethod method, string wireValue)
    {
        using var mockHttp = new MockHttpMessageHandler();
        Task<string>? requestBody = null;
        mockHttp.Expect(HttpMethod.Post, "https://analytics.babelstreet.com/rest/v1/record-similarity")
            .With(request =>
            {
                requestBody = request.Content!.ReadAsStringAsync();
                return true;
            })
            .Respond("application/json", "{}");
        using var client = mockHttp.ToHttpClient();
        var api = new ApiClient("test-key");
        api.AssignClient(client);

        var response = await CreateEndpoint().SetComparisonMethod(method).CallAsync(api);

        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(requestBody);
        using var document = JsonDocument.Parse(await requestBody!);
        Assert.Equal(wireValue, document.RootElement.GetProperty("comparisonMethod").GetString());
        mockHttp.VerifyNoOutstandingExpectation();
    }
}
