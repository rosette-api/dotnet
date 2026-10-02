using System.Text.Json;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class UnknownFieldRecordTests
{
    [Theory]
    [InlineData("{\"custom\":42}")]
    [InlineData("{\"custom\":[1,null,{\"nested\":true}]}")]
    [InlineData("[1,2]")]
    [InlineData("\"text\"")]
    [InlineData("42")]
    [InlineData("true")]
    public void UnknownField_PreservesRawJson(string json)
    {
        var field = Assert.IsType<UnknownFieldRecord>(JsonSerializer.Deserialize<UnknownFieldRecord>(json));
        Assert.Equal(json, Write(field));
        var records = new RecordSimilarityRecords(
            [new Dictionary<string, RecordSimilarityField> { ["unknown"] = field }], []);
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(records));
        Assert.Equal(json, document.RootElement.GetProperty("left")[0].GetProperty("unknown").GetRawText());
    }

    [Fact]
    public void UnknownField_WithNullData_SerializesAsJsonNull()
    {
        Assert.Equal("null", JsonSerializer.Serialize(new UnknownFieldRecord(null)));
    }
}
