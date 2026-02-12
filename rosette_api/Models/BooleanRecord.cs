using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing a boolean record
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class BooleanRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets and sets the boolean record
    /// </summary>
    [JsonPropertyName("data")]
    public bool Boolean { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public BooleanRecord() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The boolean record</param>
    public BooleanRecord(bool data)
    {
        Boolean = data;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) =>
        obj is BooleanRecord other && Boolean == other.Boolean;

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => Boolean.GetHashCode();

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => Boolean.ToString().ToLowerInvariant();
}