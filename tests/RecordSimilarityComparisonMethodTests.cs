using System.Text.Json;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class RecordSimilarityComparisonMethodTests
{
    [Theory]
    [MemberData(nameof(ComparisonMethods), MemberType = typeof(RecordSimilarityTestData))]
    public void ComparisonMethod_UsesRightJsonValues(RecordSimilarityComparisonMethod method, string wireValue)
    {
        var json = $"\"{wireValue}\"";
        Assert.Equal(json, JsonSerializer.Serialize(method));
        Assert.Equal(method, JsonSerializer.Deserialize<RecordSimilarityComparisonMethod>(json));
    }

    [Fact]
    public void ComparisonMethod_RejectsUnknownJsonValue()
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<RecordSimilarityComparisonMethod>("\"unknown\""));
    }
}
