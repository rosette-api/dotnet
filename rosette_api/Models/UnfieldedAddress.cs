using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

public class UnfieldedAddress : IAddress
{
    [JsonPropertyName("address")]
    public string? Address { get; private set; }

    /// <summary>
    /// Constructor for a Address object, used by several endpoints
    /// </summary>
    /// <param name="houseNumber">optional house number</param>
    /// <param name="road">optional road</param>
    /// <param name="city">optional city</param>
    /// <param name="state">optional state</param>
    /// <param name="postCode">optional post code</param>
    [JsonConstructor]
    public UnfieldedAddress(string? address = null)
    {
        Address = address;
    }

    /// <summary>
    /// SetAddress sets the address
    /// </summary>
    /// <param name="address">address</param>
    /// <returns>Updated Address instance</returns>
    public UnfieldedAddress SetAddress(string? address)
    {
        Address = address;
        return this;
    }

    public bool Fielded()
    {
        return false;
    }
}