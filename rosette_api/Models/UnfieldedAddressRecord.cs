using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing an unfielded address
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class UnfieldedAddressRecord : AddressField
{
    /// <summary>
    /// Gets or sets the address field's address
    /// </summary>
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public UnfieldedAddressRecord() 
    {
        Address = string.Empty;
    }

    /// <summary>
    /// Full constructor
    /// </summary>
    /// <param name="address">The address in string form</param>
    public UnfieldedAddressRecord(string address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(address);
        Address = address;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if equal</returns>
    public override bool Equals(object? obj) =>
        obj is UnfieldedAddressRecord other && Address == other.Address;

    /// <summary>
    /// Hashcode override
    /// </summary>
    /// <returns>The hashcode</returns>
    public override int GetHashCode() => Address?.GetHashCode() ?? 0;

    /// <summary>
    /// ToString override. Also used for JSON serialization
    /// </summary>
    public override string ToString() => Address;
}