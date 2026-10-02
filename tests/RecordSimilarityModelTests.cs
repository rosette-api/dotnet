using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class RecordSimilarityModelTests
{
    [Fact]
    public void OnlyWrappersAndUnknownField_ImplementRecordSimilarityField()
    {
        var implementations = typeof(RecordSimilarityField).Assembly.GetTypes()
            .Where(type => type.IsClass && typeof(RecordSimilarityField).IsAssignableFrom(type))
            .OrderBy(type => type.Name).ToArray();
        Type[] expected = [typeof(AddressRecord), typeof(BooleanRecord), typeof(DateRecord), typeof(NameRecord),
            typeof(NumberRecord), typeof(StringRecord), typeof(UnknownFieldRecord)];
        Assert.Equal(expected.OrderBy(type => type.Name), implementations);
        Assert.Equal(typeof(NameField), typeof(FieldedNameRecord).BaseType);
        Assert.Equal(typeof(NameField), typeof(UnfieldedNameRecord).BaseType);
        Assert.Equal(typeof(NameFieldData), typeof(NameField).BaseType);
        Assert.Equal(typeof(AddressField), typeof(FieldedAddressRecord).BaseType);
        Assert.Equal(typeof(AddressField), typeof(UnfieldedAddressRecord).BaseType);
        Assert.Equal(typeof(AddressFieldData), typeof(AddressField).BaseType);
        Assert.Equal(typeof(DateField), typeof(FieldedDateRecord).BaseType);
        Assert.Equal(typeof(DateField), typeof(UnfieldedDateRecord).BaseType);
        Assert.Equal(typeof(DateFieldData), typeof(DateField).BaseType);
    }
}
