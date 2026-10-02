using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models;
using Rosette.Api.Client.Models.JsonConverter;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class UnfieldedRecordSimilarityConverterTests
{
    [Fact]
    public void UnfieldedConverter_HandlesUnfieldedValuesAndUnknownFields()
    {
        var converter = new UnfieldedRecordSimilarityConverter();
        Assert.False(converter.HandleNull);
        Assert.True(converter.CanConvert(typeof(UnfieldedNameRecord)));
        Assert.True(converter.CanConvert(typeof(UnfieldedAddressRecord)));
        Assert.True(converter.CanConvert(typeof(UnfieldedDateRecord)));
        Assert.True(converter.CanConvert(typeof(UnknownFieldRecord)));
        Assert.Equal(typeof(UnfieldedRecordSimilarityConverter),
            typeof(UnknownFieldRecord).GetCustomAttribute<JsonConverterAttribute>()!.ConverterType);
        Type[] otherTypes = [typeof(NameFieldData), typeof(NameField), typeof(FieldedNameRecord), typeof(NameRecord),
            typeof(AddressFieldData), typeof(AddressField), typeof(FieldedAddressRecord), typeof(AddressRecord),
            typeof(DateFieldData), typeof(DateField), typeof(FieldedDateRecord), typeof(DateRecord),
            typeof(NumberRecord), typeof(BooleanRecord), typeof(StringRecord), typeof(object)];
        foreach (var type in otherTypes)
        {
            Assert.False(converter.CanConvert(type));
        }
    }

    [Theory]
    [MemberData(nameof(UnfieldedTypes), MemberType = typeof(RecordSimilarityTestData))]
    public void UnfieldedConverter_RejectsNonStringJson(Type type)
    {
        foreach (var json in new[] { "{}", "[]", "42", "true" })
        {
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(json, type));
        }
    }

    [Theory]
    [MemberData(nameof(UnfieldedValues), MemberType = typeof(RecordSimilarityTestData))]
    public void UnfieldedValues_SerializeAndDeserializeDirectly(Type type, string json, string propertyName, string text)
    {
        var value = Activator.CreateInstance(type)!;
        type.GetProperty(propertyName)!.SetValue(value, text);
        Assert.Equal(json, Write(value));
        var restored = Read(type, json);
        Assert.Equal(text, type.GetProperty(propertyName)!.GetValue(restored));
    }
}
