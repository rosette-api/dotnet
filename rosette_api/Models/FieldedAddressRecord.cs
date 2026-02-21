using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing a fielded address
/// </summary>
public class FieldedAddressRecord : AddressField
{
    [JsonPropertyName("house")]
    public string? House { get; set; }

    [JsonPropertyName("houseNumber")]
    public string? HouseNumber { get; set; }

    [JsonPropertyName("road")]
    public string? Road { get; set; }

    [JsonPropertyName("unit")]
    public string? Unit { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("staircase")]
    public string? Staircase { get; set; }

    [JsonPropertyName("entrance")]
    public string? Entrance { get; set; }

    [JsonPropertyName("suburb")]
    public string? Suburb { get; set; }

    [JsonPropertyName("cityDistrict")]
    public string? CityDistrict { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("island")]
    public string? Island { get; set; }

    [JsonPropertyName("stateDistrict")]
    public string? StateDistrict { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("countryRegion")]
    public string? CountryRegion { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("worldRegion")]
    public string? WorldRegion { get; set; }

    [JsonPropertyName("postcode")]
    public string? Postcode { get; set; }

    [JsonPropertyName("po_box")]
    public string? PoBox { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public FieldedAddressRecord() { }

    /// <summary>
    /// Full constructor
    /// </summary>
    public FieldedAddressRecord(
        string? house = null,
        string? houseNumber = null,
        string? road = null,
        string? unit = null,
        string? level = null,
        string? staircase = null,
        string? entrance = null,
        string? suburb = null,
        string? cityDistrict = null,
        string? city = null,
        string? island = null,
        string? stateDistrict = null,
        string? state = null,
        string? countryRegion = null,
        string? country = null,
        string? worldRegion = null,
        string? postcode = null,
        string? poBox = null)
    {
        House = house;
        HouseNumber = houseNumber;
        Road = road;
        Unit = unit;
        Level = level;
        Staircase = staircase;
        Entrance = entrance;
        Suburb = suburb;
        CityDistrict = cityDistrict;
        City = city;
        Island = island;
        StateDistrict = stateDistrict;
        State = state;
        CountryRegion = countryRegion;
        Country = country;
        WorldRegion = worldRegion;
        Postcode = postcode;
        PoBox = poBox;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) =>
        obj is FieldedAddressRecord other &&
        House == other.House &&
        HouseNumber == other.HouseNumber &&
        Road == other.Road &&
        Unit == other.Unit &&
        Level == other.Level &&
        Staircase == other.Staircase &&
        Entrance == other.Entrance &&
        Suburb == other.Suburb &&
        CityDistrict == other.CityDistrict &&
        City == other.City &&
        Island == other.Island &&
        StateDistrict == other.StateDistrict &&
        State == other.State &&
        CountryRegion == other.CountryRegion &&
        Country == other.Country &&
        WorldRegion == other.WorldRegion &&
        Postcode == other.Postcode &&
        PoBox == other.PoBox;

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(House);
        hash.Add(HouseNumber);
        hash.Add(Road);
        hash.Add(Unit);
        hash.Add(Level);
        hash.Add(Staircase);
        hash.Add(Entrance);
        hash.Add(Suburb);
        hash.Add(CityDistrict);
        hash.Add(City);
        hash.Add(Island);
        hash.Add(StateDistrict);
        hash.Add(State);
        hash.Add(CountryRegion);
        hash.Add(Country);
        hash.Add(WorldRegion);
        hash.Add(Postcode);
        hash.Add(PoBox);
        return hash.ToHashCode();
    }

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}