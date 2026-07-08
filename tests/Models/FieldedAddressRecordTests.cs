using Rosette.Api.Client.Models;
using System.Text.Json;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for FieldedAddressRecord class
/// </summary>
public class FieldedAddressRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_CreatesEmptyRecord()
    {
        // Act
        var record = new FieldedAddressRecord();

        // Assert
        Assert.Null(record.House);
        Assert.Null(record.HouseNumber);
        Assert.Null(record.Road);
        Assert.Null(record.City);
        Assert.Null(record.State);
        Assert.Null(record.Country);
        Assert.Null(record.Postcode);
    }

    [Fact]
    public void Constructor_WithAllParameters_SetsAllProperties()
    {
        // Act
        var record = new FieldedAddressRecord(
            house: "White House",
            houseNumber: "1600",
            road: "Pennsylvania Avenue NW",
            city: "Washington",
            state: "DC",
            country: "USA",
            postcode: "20500"
        );

        // Assert
        Assert.Equal("White House", record.House);
        Assert.Equal("1600", record.HouseNumber);
        Assert.Equal("Pennsylvania Avenue NW", record.Road);
        Assert.Equal("Washington", record.City);
        Assert.Equal("DC", record.State);
        Assert.Equal("USA", record.Country);
        Assert.Equal("20500", record.Postcode);
    }

    [Fact]
    public void Constructor_WithSubsetOfParameters_SetsSpecifiedProperties()
    {
        // Act
        var record = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main Street",
            city: "Springfield"
        );

        // Assert
        Assert.Equal("123", record.HouseNumber);
        Assert.Equal("Main Street", record.Road);
        Assert.Equal("Springfield", record.City);
        Assert.Null(record.House);
        Assert.Null(record.State);
        Assert.Null(record.Country);
    }

    #endregion

    #region Property Tests - Basic Fields

    [Fact]
    public void House_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.House = "Empire State Building";

        // Assert
        Assert.Equal("Empire State Building", record.House);
    }

    [Fact]
    public void HouseNumber_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.HouseNumber = "350";

        // Assert
        Assert.Equal("350", record.HouseNumber);
    }

    [Fact]
    public void Road_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Road = "5th Avenue";

        // Assert
        Assert.Equal("5th Avenue", record.Road);
    }

    [Fact]
    public void City_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.City = "New York";

        // Assert
        Assert.Equal("New York", record.City);
    }

    [Fact]
    public void State_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.State = "NY";

        // Assert
        Assert.Equal("NY", record.State);
    }

    [Fact]
    public void Country_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Country = "United States";

        // Assert
        Assert.Equal("United States", record.Country);
    }

    [Fact]
    public void Postcode_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Postcode = "10118";

        // Assert
        Assert.Equal("10118", record.Postcode);
    }

    #endregion

    #region Property Tests - Extended Fields

    [Fact]
    public void Unit_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Unit = "Apt 3B";

        // Assert
        Assert.Equal("Apt 3B", record.Unit);
    }

    [Fact]
    public void Level_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Level = "Floor 5";

        // Assert
        Assert.Equal("Floor 5", record.Level);
    }

    [Fact]
    public void Staircase_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Staircase = "Staircase A";

        // Assert
        Assert.Equal("Staircase A", record.Staircase);
    }

    [Fact]
    public void Entrance_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Entrance = "North Entrance";

        // Assert
        Assert.Equal("North Entrance", record.Entrance);
    }

    [Fact]
    public void Suburb_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Suburb = "Brooklyn";

        // Assert
        Assert.Equal("Brooklyn", record.Suburb);
    }

    [Fact]
    public void CityDistrict_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.CityDistrict = "Manhattan";

        // Assert
        Assert.Equal("Manhattan", record.CityDistrict);
    }

    [Fact]
    public void Island_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.Island = "Long Island";

        // Assert
        Assert.Equal("Long Island", record.Island);
    }

    [Fact]
    public void StateDistrict_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.StateDistrict = "Southern District";

        // Assert
        Assert.Equal("Southern District", record.StateDistrict);
    }

    [Fact]
    public void CountryRegion_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.CountryRegion = "New England";

        // Assert
        Assert.Equal("New England", record.CountryRegion);
    }

    [Fact]
    public void WorldRegion_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.WorldRegion = "North America";

        // Assert
        Assert.Equal("North America", record.WorldRegion);
    }

    [Fact]
    public void PoBox_CanBeSetAndRetrieved()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        record.PoBox = "PO Box 123";

        // Assert
        Assert.Equal("PO Box 123", record.PoBox);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameProperties_ReturnsTrue()
    {
        // Arrange
        var record1 = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main St",
            city: "Springfield",
            state: "IL",
            country: "USA",
            postcode: "62701"
        );
        var record2 = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main St",
            city: "Springfield",
            state: "IL",
            country: "USA",
            postcode: "62701"
        );

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_AllNullProperties_ReturnsTrue()
    {
        // Arrange
        var record1 = new FieldedAddressRecord();
        var record2 = new FieldedAddressRecord();

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentHouseNumber_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedAddressRecord(houseNumber: "123");
        var record2 = new FieldedAddressRecord(houseNumber: "456");

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentCity_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedAddressRecord(city: "Springfield");
        var record2 = new FieldedAddressRecord(city: "Boston");

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_OnePropertySetOneNull_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedAddressRecord(city: "Springfield");
        var record2 = new FieldedAddressRecord();

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new FieldedAddressRecord();
        var differentType = "address";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main St",
            city: "Springfield"
        );
        var record2 = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main St",
            city: "Springfield"
        );

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentProperties_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new FieldedAddressRecord(city: "Springfield");
        var record2 = new FieldedAddressRecord(city: "Boston");

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsValidJson()
    {
        // Arrange
        var record = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main St",
            city: "Springfield"
        );

        // Act
        string json = record.ToString();

        // Assert
        Assert.NotEmpty(json);
        Assert.Contains("123", json);
        Assert.Contains("Main St", json);
        Assert.Contains("Springfield", json);
    }

    [Fact]
    public void ToString_CanBeDeserialized()
    {
        // Arrange
        var original = new FieldedAddressRecord(
            houseNumber: "123",
            road: "Main St",
            city: "Springfield",
            state: "IL",
            country: "USA",
            postcode: "62701"
        );

        // Act
        string json = original.ToString();
        var deserialized = JsonSerializer.Deserialize<FieldedAddressRecord>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.HouseNumber, deserialized.HouseNumber);
        Assert.Equal(original.Road, deserialized.Road);
        Assert.Equal(original.City, deserialized.City);
        Assert.Equal(original.State, deserialized.State);
        Assert.Equal(original.Country, deserialized.Country);
        Assert.Equal(original.Postcode, deserialized.Postcode);
    }

    [Fact]
    public void ToString_EmptyRecord_ProducesValidJson()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        string json = record.ToString();
        var deserialized = JsonSerializer.Deserialize<FieldedAddressRecord>(json);

        // Assert
        Assert.NotNull(deserialized);
    }

    #endregion
}
