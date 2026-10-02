using System.Text.Json;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class AddressRecordTests
{
    [Fact]
    public void FieldedAddresses_PreserveAllComponentsAndRespectIgnoreNullOptions()
    {
        var properties = typeof(FieldedAddressRecord).GetProperties().Where(property => property.PropertyType == typeof(string)).ToArray();
        Assert.Equal(18, properties.Length);
        var address = new FieldedAddressRecord();
        foreach (var property in properties) property.SetValue(address, property.Name);
        var field = new AddressRecord([address]);
        Assert.Equal(field, JsonSerializer.Deserialize<AddressRecord>(Write(field)));
        Assert.Equal("{\"road\":\"Main St\"}", Write(new AddressRecord([new FieldedAddressRecord { Road = "Main St" }])));
        Assert.IsType<FieldedAddressRecord>(JsonSerializer.Deserialize<AddressRecord>(Write(field))!.Data[0]);
    }

}

