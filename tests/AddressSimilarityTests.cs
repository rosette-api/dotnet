using System.Text.Json;
using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class AddressSimilarityTests
{
    [Fact]
    public void AddressSimilarity_SerializesIndividualValuesNotWrappers()
    {
        var endpoint = new AddressSimilarity(
            new UnfieldedAddressRecord { Address = "123 Main St" },
            new FieldedAddressRecord { HouseNumber = "123", Road = "Main St" });
        var json = JsonSerializer.Serialize(endpoint.Params);
        using var document = JsonDocument.Parse(json);
        Assert.Equal("123 Main St", document.RootElement.GetProperty("address1").GetString());
        Assert.Equal("123", document.RootElement.GetProperty("address2").GetProperty("houseNumber").GetString());
        Assert.Equal("Main St", document.RootElement.GetProperty("address2").GetProperty("road").GetString());
    }
}
