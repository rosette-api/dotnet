using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class FieldedUnfieldedRecordTests
{
    public static IEnumerable<object[]> ConstructorCases =>
    [
        [typeof(FieldedNameRecord), new object?[] { "Alice", "eng", null, null, "PERSON" },
            new[] { "Text", "Language", "EntityType" }, new string?[] { "Alice", "eng", "PERSON" }],
        [typeof(UnfieldedNameRecord), new object?[] { "Alice" }, new[] { "Text" }, new string?[] { "Alice" }],
        [typeof(FieldedAddressRecord), new object?[]
            { null, "123", "Main St", null, null, null, null, null, null, null, null, "District", "DC", null, null, null, null, null },
            new[] { "HouseNumber", "Road", "StateDistrict", "State" }, new string?[] { "123", "Main St", "District", "DC" }],
        [typeof(UnfieldedAddressRecord), new object?[] { "123 Main St" }, new[] { "Address" }, new string?[] { "123 Main St" }],
        [typeof(FieldedDateRecord), new object?[] { "04161993", "MMddyyyy" }, new[] { "Date", "Format" }, new string?[] { "04161993", "MMddyyyy" }],
        [typeof(UnfieldedDateRecord), new object?[] { "1993-04-16" }, new[] { "Date" }, new string?[] { "1993-04-16" }],
        [typeof(FieldedDateRecord), new object?[] { "1993-04-16", null }, new[] { "Date", "Format" }, new string?[] { "1993-04-16", null }]
    ];

    [Theory]
    [MemberData(nameof(ConstructorCases))]
    public void ValueConstructors_PreservePropertiesAndEquality(Type type, object?[] arguments, string[] propertyNames, string?[] propertyValues)
    {
        var actual = Activator.CreateInstance(type, arguments)!;
        var expected = Activator.CreateInstance(type)!;
        for (var index = 0; index < propertyNames.Length; index++)
        {
            type.GetProperty(propertyNames[index])!.SetValue(expected, propertyValues[index]);
        }
        Assert.Equal(expected, actual);
        Assert.Equal(expected.GetHashCode(), actual.GetHashCode());
    }
}

