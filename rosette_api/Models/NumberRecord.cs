using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing a number record
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class NumberRecord : RecordSimilarityField
{
    public const string DATA = "data";

    /// <summary>
    /// Gets and sets the number record
    /// </summary>
    [JsonPropertyName("data")]
    public double Number { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public NumberRecord() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The number record</param>
    public NumberRecord(double data)
    {
        Number = data;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) =>
        obj is NumberRecord other && Number == other.Number;

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => Number.GetHashCode();

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => Number.ToString();
}