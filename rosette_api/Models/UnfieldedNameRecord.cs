using rosette_api.Models.JsonConverter;
using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models;

/// <summary>
/// Class for representing an unfielded name
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class UnfieldedNameRecord : NameField
{
    /// <summary>
    /// No-args constructor
    /// </summary>
    public UnfieldedNameRecord() 
    {
        Text = string.Empty;
    }

    /// <summary>
    /// Full constructor
    /// </summary>
    /// <param name="text">The name as a string</param>
    public UnfieldedNameRecord(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        Text = text;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if equal</returns>
    public override bool Equals(object? obj) =>
        obj is UnfieldedNameRecord other && Text == other.Text;

    /// <summary>
    /// Hashcode override
    /// </summary>
    /// <returns>The hashcode</returns>
    public override int GetHashCode() => Text?.GetHashCode() ?? 0;

    /// <summary>
    /// ToString override. Also used for JSON serialization
    /// </summary>
    public override string ToString() => Text;
}