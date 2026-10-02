using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public static class RecordSimilarityTestData
{
    public static IEnumerable<object[]> Fields =>
    [
        [typeof(NameRecord), "\"Alice\"", "[\"Alice\",{\"text\":\"Alicia\",\"language\":\"spa\"}]"],
        [typeof(AddressRecord), "\"123 Main St\"", "[\"123 Main St\",{\"houseNumber\":\"123\",\"road\":\"Main St\",\"postCode\":\"20500\",\"poBox\":\"42\"}]"],
        [typeof(DateRecord), "\"1993-04-16\"", "[\"1993-04-16\",{\"date\":\"04161993\",\"format\":\"MMddyyyy\"}]"],
        [typeof(BooleanRecord), "false", "[false,true]"],
        [typeof(NumberRecord), "47.344", "[47.344,72]"],
        [typeof(StringRecord), "\"manager\"", "[\"manager\",\"engineer\"]"]
    ];

    public static IEnumerable<object[]> SingleFields => Fields.Select(row => row.Take(2).ToArray());

    public static IEnumerable<object[]> StructuredValues =>
    [
        [typeof(NameFieldData), typeof(NameRecord), typeof(FieldedNameRecord), typeof(UnfieldedNameRecord), "\"Alice\"", "{\"text\":\"Alice\"}"],
        [typeof(AddressFieldData), typeof(AddressRecord), typeof(FieldedAddressRecord), typeof(UnfieldedAddressRecord), "\"Main St\"", "{\"road\":\"Main St\"}"],
        [typeof(DateFieldData), typeof(DateRecord), typeof(FieldedDateRecord), typeof(UnfieldedDateRecord), "\"1993-04-16\"", "{\"date\":\"1993-04-16\"}"]
    ];

    public static IEnumerable<object[]> FieldedWrapperValues => StructuredValues.Select(row => new[] { row[1], row[2], row[5] });
    public static IEnumerable<object[]> FieldDataValues => StructuredValues.Select(row => new[] { row[0], row[2], row[3], row[4], row[5] });

    public static IEnumerable<object[]> UnfieldedValues =>
    [
        [typeof(UnfieldedNameRecord), "\"Alice\"", "Text", "Alice"],
        [typeof(UnfieldedAddressRecord), "\"123 Main St\"", "Address", "123 Main St"],
        [typeof(UnfieldedDateRecord), "\"1993-04-16\"", "Date", "1993-04-16"]
    ];

    public static IEnumerable<object[]> UnfieldedTypes => UnfieldedValues.Select(row => new[] { row[0] });

    public static IEnumerable<object[]> ReferenceTypes =>
        Fields.Select(row => new[] { row[0] }).Concat(UnfieldedTypes).Append([typeof(UnknownFieldRecord)]);

    public static IEnumerable<object[]> ModelTypes =>
        ReferenceTypes.Concat(AbstractDataTypes).Concat(new object[][]
        {
            [typeof(FieldedNameRecord)], [typeof(FieldedAddressRecord)], [typeof(FieldedDateRecord)]
        });

    public static IEnumerable<object[]> AbstractDataTypes =>
    [
        [typeof(NameFieldData)], [typeof(NameField)], [typeof(AddressFieldData)],
        [typeof(AddressField)], [typeof(DateFieldData)], [typeof(DateField)]
    ];

    public static JsonSerializerOptions IgnoreNullOptions { get; } = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Invoke the existing constructors without changing their required-member annotations.
    public static T Construct<T>(params object?[] arguments) => (T)Activator.CreateInstance(typeof(T), arguments)!;
    public static object Read(Type type, string json) => JsonSerializer.Deserialize(json, type)!;
    public static string Write(object field) => JsonSerializer.Serialize(field, field.GetType(), IgnoreNullOptions);
    public static PropertyInfo DataProperty(Type type) => type.GetProperty("Data")!;
    public static IList Data(object field) => (IList)DataProperty(field.GetType()).GetValue(field)!;
}
