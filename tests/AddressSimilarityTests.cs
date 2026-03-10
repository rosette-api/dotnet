using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class AddressSimilarityTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpoint_WhenCalledWithUnfieldedAddresses()
    {
        var address1 = new UnfieldedAddressRecord { Address = "1600 Pennsylvania Avenue, Washington, D.C., 20500" };
        var address2 = new UnfieldedAddressRecord { Address = "160 Pennsylvana Avenue, Washington, D.C., 20500" };

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCalledWithFieldedAddresses()
    {
        var address1 = new FieldedAddressRecord(
            houseNumber: "1600",
            road: "Pennsylvania Avenue NW",
            city: "Washington",
            state: "DC",
            postcode: "20500"
        );
        var address2 = new FieldedAddressRecord(
            houseNumber: "1600",
            road: "Pennsylvania Ave N.W.",
            city: "Washington",
            state: "DC",
            postcode: "20500"
        );

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCalledWithMixedAddresses()
    {
        var address1 = new UnfieldedAddressRecord { Address = "1600 Pennsylvania Avenue, Washington, D.C., 20500" };
        var address2 = new FieldedAddressRecord(
            houseNumber: "1600",
            road: "Pennsylvania Avenue NW",
            city: "Washington",
            state: "DC",
            postcode: "20500"
        );

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenAddress1IsNull()
    {
        var address2 = new UnfieldedAddressRecord { Address = "Some address" };

        Assert.Throws<ArgumentNullException>(() => new AddressSimilarity(null!, address2));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenAddress2IsNull()
    {
        var address1 = new UnfieldedAddressRecord { Address = "Some address" };

        Assert.Throws<ArgumentNullException>(() => new AddressSimilarity(address1, null!));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenBothAddressesAreNull()
    {
        Assert.Throws<ArgumentNullException>(() => new AddressSimilarity(null!, null!));
    }

    #endregion

    #region Parameter Tests

    [Fact]
    public void Constructor_AddsAddress1ToParams_WhenCalled()
    {
        var address1 = new UnfieldedAddressRecord { Address = "Address 1" };
        var address2 = new UnfieldedAddressRecord { Address = "Address 2" };

        AddressSimilarity asim = new(address1, address2);

        Assert.Contains("address1", asim.Params.Keys);
        Assert.Equal(address1, asim.Params["address1"]);
    }

    [Fact]
    public void Constructor_AddsAddress2ToParams_WhenCalled()
    {
        var address1 = new UnfieldedAddressRecord { Address = "Address 1" };
        var address2 = new UnfieldedAddressRecord { Address = "Address 2" };

        AddressSimilarity asim = new(address1, address2);

        Assert.Contains("address2", asim.Params.Keys);
        Assert.Equal(address2, asim.Params["address2"]);
    }

    #endregion

    #region UnfieldedAddressRecord Tests

    [Fact]
    public void Constructor_AcceptsUnfieldedAddress_WithSimpleAddress()
    {
        var address1 = new UnfieldedAddressRecord { Address = "123 Main St" };
        var address2 = new UnfieldedAddressRecord { Address = "123 Main Street" };

        AddressSimilarity asim = new(address1, address2);

        var storedAddress1 = (UnfieldedAddressRecord)asim.Params["address1"];
        Assert.Equal("123 Main St", storedAddress1.Address);
    }

    [Fact]
    public void Constructor_AcceptsUnfieldedAddress_WithComplexAddress()
    {
        var address1 = new UnfieldedAddressRecord 
        { 
            Address = "Suite 500, 1234 Technology Drive, San Francisco, CA 94102, United States" 
        };
        var address2 = new UnfieldedAddressRecord 
        { 
            Address = "1234 Technology Dr, Suite 500, San Francisco, California 94102" 
        };

        AddressSimilarity asim = new(address1, address2);

        Assert.NotNull(asim.Params["address1"]);
        Assert.NotNull(asim.Params["address2"]);
    }

    [Fact]
    public void Constructor_AcceptsUnfieldedAddress_WithInternationalAddress()
    {
        var address1 = new UnfieldedAddressRecord { Address = "10 Downing Street, London, SW1A 2AA, UK" };
        var address2 = new UnfieldedAddressRecord { Address = "10 Downing St, Westminster, London SW1A 2AA" };

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    #endregion

    #region FieldedAddressRecord Tests

    [Fact]
    public void Constructor_AcceptsFieldedAddress_WithAllFields()
    {
        var address1 = new FieldedAddressRecord(
            houseNumber: "1600",
            road: "Pennsylvania Avenue NW",
            city: "Washington",
            state: "DC",
            postcode: "20500",
            country: "United States"
        );
        var address2 = new FieldedAddressRecord(
            houseNumber: "1600",
            road: "Pennsylvania Ave",
            city: "Washington",
            state: "DC",
            postcode: "20500"
        );

        AddressSimilarity asim = new(address1, address2);

        var storedAddress1 = (FieldedAddressRecord)asim.Params["address1"];
        Assert.Equal("1600", storedAddress1.HouseNumber);
        Assert.Equal("Pennsylvania Avenue NW", storedAddress1.Road);
    }

    [Fact]
    public void Constructor_AcceptsFieldedAddress_WithMinimalFields()
    {
        var address1 = new FieldedAddressRecord(city: "Boston");
        var address2 = new FieldedAddressRecord(city: "Boston");

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        var address1 = new UnfieldedAddressRecord { Address = "Address 1" };
        var address2 = new UnfieldedAddressRecord { Address = "Address 2" };

        AddressSimilarity asim = new AddressSimilarity(address1, address2)
            .SetOption("explain", true);

        Assert.True((bool)asim.Options["explain"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        var address1 = new UnfieldedAddressRecord { Address = "Address 1" };
        var address2 = new UnfieldedAddressRecord { Address = "Address 2" };

        AddressSimilarity asim = new AddressSimilarity(address1, address2)
            .SetOption("explain", true)
            .SetOption("debug", "detailed");

        Assert.True((bool)asim.Options["explain"]);
        Assert.Equal("detailed", asim.Options["debug"]);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingOptions()
    {
        var address1 = new UnfieldedAddressRecord { Address = "123 Main St" };
        var address2 = new UnfieldedAddressRecord { Address = "123 Main Street" };

        AddressSimilarity asim = new AddressSimilarity(address1, address2)
            .SetOption("explain", true);

        Assert.True((bool)asim.Options["explain"]);
    }

    #endregion

    #region Real-World Address Tests

    [Fact]
    public void Constructor_HandlesTypicalUSAddress_WhenCalled()
    {
        var address1 = new UnfieldedAddressRecord { Address = "1 Apple Park Way, Cupertino, CA 95014" };
        var address2 = new FieldedAddressRecord(
            houseNumber: "1",
            road: "Apple Park Way",
            city: "Cupertino",
            state: "CA",
            postcode: "95014"
        );

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    [Fact]
    public void Constructor_HandlesAbbreviationsAndSpellingVariations_WhenCalled()
    {
        var address1 = new UnfieldedAddressRecord { Address = "1600 Penn Ave NW" };
        var address2 = new UnfieldedAddressRecord { Address = "1600 Pennsylvania Avenue Northwest" };

        AddressSimilarity asim = new(address1, address2);

        Assert.Equal("address-similarity", asim.Endpoint);
    }

    #endregion
}
