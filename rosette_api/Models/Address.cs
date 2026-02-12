using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

public class Address : IAddress
{
    [JsonPropertyName("houseNumber")]
    public string? HouseNumber { get; private set; }

    [JsonPropertyName("road")]
    public string? Road { get; private set; }

    [JsonPropertyName("city")]
    public string? City { get; private set; }

    [JsonPropertyName("state")]
    public string? State { get; private set; }

    [JsonPropertyName("postCode")]
    public string? PostCode { get; private set; }

    /// <summary>
    /// Constructor for a Address object, used by several endpoints
    /// </summary>
    /// <param name="houseNumber">optional house number</param>
    /// <param name="road">optional road</param>
    /// <param name="city">optional city</param>
    /// <param name="state">optional state</param>
    /// <param name="postCode">optional post code</param>
    [JsonConstructor]
    public Address(string? houseNumber = null, string? road = null, string? city = null, string? state = null, string? postCode = null)
    {
        HouseNumber = houseNumber;
        Road = road;
        City = city;
        State = state;
        PostCode = postCode;
    }

    /// <summary>
    /// SetHouseNumber sets the house number
    /// </summary>
    /// <param name="houseNumber">house number</param>
    /// <returns>Updated Address instance</returns>
    public Address SetHouseNumber(string? houseNumber)
    {
        HouseNumber = houseNumber;
        return this;
    }

    /// <summary>
    /// SetRoad sets the road
    /// </summary>
    /// <param name="road">road</param>
    /// <returns>Updated Address instance</returns>
    public Address SetRoad(string? road)
    {
        Road = road;
        return this;
    }

    /// <summary>
    /// SetCity sets the city
    /// </summary>
    /// <param name="city">city</param>
    /// <returns>Updated Address instance</returns>
    public Address SetCity(string? city)
    {
        City = city;
        return this;
    }

    /// <summary>
    /// SetState sets the state
    /// </summary>
    /// <param name="state">state</param>
    /// <returns>Updated Address instance</returns>
    public Address SetState(string? state)
    {
        State = state;
        return this;
    }

    /// <summary>
    /// SetPostCode sets the post code
    /// </summary>
    /// <param name="postCode">post code</param>
    /// <returns>Updated Address instance</returns>
    public Address SetPostCode(string? postCode)
    {
        PostCode = postCode;
        return this;
    }

    public bool Fielded()
    {
        return true;
    }
}