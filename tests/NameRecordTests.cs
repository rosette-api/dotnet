using System.Text.Json;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class NameRecordTests
{
    [Fact]
    public void FieldedNames_PreserveMetadataAndRespectIgnoreNullOptions()
    {
        var name = new FieldedNameRecord
        {
            Text = "Alice", Language = "eng", LanguageOfOrigin = "eng", Script = "Latn", EntityType = "PERSON"
        };
        var field = new NameRecord([name]);
        Assert.Equal(field, JsonSerializer.Deserialize<NameRecord>(Write(field)));
        Assert.Equal("{\"text\":\"Alice\"}", Write(new NameRecord([new FieldedNameRecord { Text = "Alice" }])));
        Assert.IsType<FieldedNameRecord>(JsonSerializer.Deserialize<NameRecord>(Write(field))!.Data[0]);
    }

    [Fact]
    public void FieldedAndUnfieldedNames_AreNotEqual()
    {
        var fielded = Construct<FieldedNameRecord>("Alice", null, null, null, null);
        Assert.False(fielded.Equals(Construct<UnfieldedNameRecord>("Alice")));
    }
}


