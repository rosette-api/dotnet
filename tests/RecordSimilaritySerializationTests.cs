using System.Text.Json;
using System.Text.Json.Nodes;
using Rosette.Api.Client.Models;
using Rosette.Api.Client.Models.JsonConverter;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class RecordSimilaritySerializationTests
{
    [Theory]
    [MemberData(nameof(Fields), MemberType = typeof(RecordSimilarityTestData))]
    public void Fields_RoundTripScalarSingletonArrayAndMultipleValues(Type type, string single, string multiple)
    {
        var singleton = Read(type, single);
        Assert.Single(Data(singleton));
        Assert.Equal(single, Write(singleton));
        Assert.Equal(single, singleton.ToString());
        Assert.Equal(singleton, Read(type, $"[{single}]"));
        Assert.Equal(single, Write(Read(type, $"[{single}]")));

        var collection = Read(type, multiple);
        Assert.Equal(2, Data(collection).Count);
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse(multiple), JsonNode.Parse(Write(collection))));
        var equal = Read(type, Write(collection));
        Assert.Equal(collection, equal);
        Assert.Equal(collection.GetHashCode(), equal.GetHashCode());
        Assert.NotEqual(singleton, collection);

        var reversed = (JsonArray)JsonNode.Parse(multiple)!;
        var reversedJson = new JsonArray(reversed[1]!.DeepClone(), reversed[0]!.DeepClone()).ToJsonString();
        Assert.NotEqual(collection, Read(type, reversedJson));
    }

    [Theory]
    [MemberData(nameof(SingleFields), MemberType = typeof(RecordSimilarityTestData))]
    public void Deserialization_RejectsEmptyNullElementAndNestedArrays(Type type, string single)
    {
        foreach (var json in new[] { "[]", "[null]", $"[[{single}]]" })
        {
            Assert.Throws<JsonException>(() => Read(type, json));
        }
    }

    [Theory]
    [InlineData(typeof(NameRecord), "42")]
    [InlineData(typeof(NameRecord), "true")]
    [InlineData(typeof(NameRecord), "{}")]
    [InlineData(typeof(NameRecord), "{\"text\":null}")]
    [InlineData(typeof(AddressRecord), "42")]
    [InlineData(typeof(AddressRecord), "false")]
    [InlineData(typeof(DateRecord), "42")]
    [InlineData(typeof(DateRecord), "{}")]
    [InlineData(typeof(DateRecord), "{\"date\":null}")]
    [InlineData(typeof(DateRecord), "{\"format\":\"yyyy-MM-dd\"}")]
    [InlineData(typeof(BooleanRecord), "\"false\"")]
    [InlineData(typeof(BooleanRecord), "0")]
    [InlineData(typeof(NumberRecord), "\"47\"")]
    [InlineData(typeof(NumberRecord), "true")]
    [InlineData(typeof(StringRecord), "42")]
    [InlineData(typeof(StringRecord), "{}")]
    public void Deserialization_RejectsWrongElementTypes(Type type, string json)
    {
        Assert.Throws<JsonException>(() => Read(type, json));
        Assert.Throws<JsonException>(() => Read(type, $"[{json}]"));
    }

    [Theory]
    [InlineData(typeof(NameRecord), "\"Alice\"", "Text")]
    [InlineData(typeof(NameRecord), "{\"text\":\"Alice\"}", "Text")]
    [InlineData(typeof(AddressRecord), "\"123 Main St\"", "Address")]
    [InlineData(typeof(DateRecord), "\"1993-04-16\"", "Date")]
    [InlineData(typeof(DateRecord), "{\"date\":\"1993-04-16\"}", "Date")]
    public void Serialization_RejectsNullPayloadsAfterMutation(Type type, string json, string propertyName)
    {
        var field = Read(type, json);
        var value = Data(field)[0]!;
        value.GetType().GetProperty(propertyName)!.SetValue(value, null);

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(field, type));
        Assert.Throws<JsonException>(() => Write(field));
        Assert.Throws<JsonException>(() => field.ToString());

        Data(field).Add(Data(Read(type, json))[0]);
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(field, type));
        Assert.Throws<JsonException>(() => Write(field));
    }

    [Theory]
    [MemberData(nameof(Fields), MemberType = typeof(RecordSimilarityTestData))]
    public void RecordDictionariesAndInterfaceSerialization_UseConcreteConverters(Type type, string single, string multiple)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new RecordSimilarityFieldConverter());
        RecordSimilarityField one = (RecordSimilarityField)Read(type, single);
        RecordSimilarityField many = (RecordSimilarityField)Read(type, multiple);
        Assert.Equal(single, JsonSerializer.Serialize(one, options));
        var records = new RecordSimilarityRecords(
            [new Dictionary<string, RecordSimilarityField> { ["field"] = one }],
            [new Dictionary<string, RecordSimilarityField> { ["field"] = many }]);
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(records));
        Assert.Equal(single, document.RootElement.GetProperty("left")[0].GetProperty("field").GetRawText());
        Assert.Equal(2, document.RootElement.GetProperty("right")[0].GetProperty("field").GetArrayLength());
    }

    [Theory]
    [MemberData(nameof(FieldedWrapperValues), MemberType = typeof(RecordSimilarityTestData))]
    public void Wrappers_PreserveFieldedValuesDefaultSerialization(Type wrapperType, Type valueType, string json)
    {
        var value = Read(valueType, json);
        var wrapper = Read(wrapperType, json);
        var valueJson = JsonSerializer.Serialize(value, valueType);
        var wrapperJson = JsonSerializer.Serialize(wrapper, wrapperType);
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse(valueJson), JsonNode.Parse(wrapperJson)));
        Assert.Equal(wrapper, Read(wrapperType, wrapperJson));
    }

    [Theory]
    [MemberData(nameof(ReferenceTypes), MemberType = typeof(RecordSimilarityTestData))]
    public void NullReferences_UseDefaultSerializerBehavior(Type type)
    {
        Assert.Null(JsonSerializer.Deserialize("null", type));
        Assert.Equal("null", JsonSerializer.Serialize((object?)null, type));
    }
}
