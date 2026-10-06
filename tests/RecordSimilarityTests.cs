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

    [Theory]
    [MemberData(nameof(ComparisonMethods), MemberType = typeof(RecordSimilarityTestData))]
    public async Task CallAsync_SendsPopulatedRecords(RecordSimilarityComparisonMethod method, string wireValue)
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            ["name"] = new() { Type = RecordFieldType.RniName },
            ["address"] = new() { Type = RecordFieldType.RniAddress },
            ["date"] = new() { Type = RecordFieldType.RniDate },
            ["boolean"] = new() { Type = RecordFieldType.RniBoolean },
            ["number"] = new() { Type = RecordFieldType.RniNumber },
            ["string"] = new() { Type = RecordFieldType.RniString }
        };
        var records = new RecordSimilarityRecords(
            [new Dictionary<string, RecordSimilarityField>
            {
                ["name"] = new NameRecord([
                    new UnfieldedNameRecord { Text = "Alice" },
                    new FieldedNameRecord { Text = "Alicia", Language = "spa" }]),
                ["address"] = new AddressRecord([
                    new UnfieldedAddressRecord { Address = "123 Main St" },
                    new FieldedAddressRecord { HouseNumber = "123", Road = "Main St" }]),
                ["date"] = new DateRecord([
                    new UnfieldedDateRecord { Date = "1993-04-16" },
                    new FieldedDateRecord { Date = "04161993", Format = "MMddyyyy" }]),
                ["boolean"] = new BooleanRecord([false, true]),
                ["number"] = new NumberRecord([47.344, 72]),
                ["string"] = new StringRecord(["manager", "engineer"])
            }],
            [new Dictionary<string, RecordSimilarityField>
            {
                ["name"] = new NameRecord([new FieldedNameRecord { Text = "Alice" }]),
                ["address"] = new AddressRecord([new UnfieldedAddressRecord { Address = "123 Main St" }]),
                ["date"] = new DateRecord([new FieldedDateRecord { Date = "1993-04-16" }]),
                ["boolean"] = new BooleanRecord([false]),
                ["number"] = new NumberRecord([47.344]),
                ["string"] = new StringRecord(["manager"])
            }]);
        var endpoint = new RecordSimilarity(fields, new RecordSimilarityProperties { Threshold = 0.7 }, records, method);

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

        var response = await endpoint.CallAsync(api);

        Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(requestBody);
        using var document = JsonDocument.Parse(await requestBody!);
        var root = document.RootElement;
        Assert.Equal(wireValue, root.GetProperty("comparisonMethod").GetString());
        Assert.Equal("rni_name", root.GetProperty("fields").GetProperty("name").GetProperty("type").GetString());
        Assert.Equal(0.7, root.GetProperty("properties").GetProperty("threshold").GetDouble());
        var leftRecords = root.GetProperty("records").GetProperty("left");
        var rightRecords = root.GetProperty("records").GetProperty("right");
        Assert.Equal(1, leftRecords.GetArrayLength());
        Assert.Equal(1, rightRecords.GetArrayLength());
        var left = leftRecords[0];
        var right = rightRecords[0];
        foreach (var field in fields.Keys)
        {
            Assert.Equal(2, left.GetProperty(field).GetArrayLength());
        }
        Assert.Equal("Alice", left.GetProperty("name")[0].GetString());
        Assert.Equal("Alicia", left.GetProperty("name")[1].GetProperty("text").GetString());
        Assert.Equal("spa", left.GetProperty("name")[1].GetProperty("language").GetString());
        Assert.Equal("123 Main St", left.GetProperty("address")[0].GetString());
        Assert.Equal("123", left.GetProperty("address")[1].GetProperty("houseNumber").GetString());
        Assert.Equal("Main St", left.GetProperty("address")[1].GetProperty("road").GetString());
        Assert.Equal("1993-04-16", left.GetProperty("date")[0].GetString());
        Assert.Equal("04161993", left.GetProperty("date")[1].GetProperty("date").GetString());
        Assert.Equal("MMddyyyy", left.GetProperty("date")[1].GetProperty("format").GetString());
        Assert.Equal("[false,true]", left.GetProperty("boolean").GetRawText());
        Assert.Equal("[47.344,72]", left.GetProperty("number").GetRawText());
        Assert.Equal("[\"manager\",\"engineer\"]", left.GetProperty("string").GetRawText());
        Assert.Equal("Alice", right.GetProperty("name").GetProperty("text").GetString());
        Assert.Equal("123 Main St", right.GetProperty("address").GetString());
        Assert.Equal("1993-04-16", right.GetProperty("date").GetProperty("date").GetString());
        Assert.False(right.GetProperty("boolean").GetBoolean());
        Assert.Equal(47.344, right.GetProperty("number").GetDouble());
        Assert.Equal("manager", right.GetProperty("string").GetString());
        mockHttp.VerifyNoOutstandingExpectation();
    }
}
