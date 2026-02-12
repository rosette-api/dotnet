using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing a string record
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class StringRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets and sets the string record
    /// </summary>
    [JsonPropertyName("data")]
    public required string Text { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public StringRecord() 
    {
        Text = string.Empty;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The string record</param>
    public StringRecord(string data)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(data);
        Text = data;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) =>
        obj is StringRecord other && Text == other.Text;

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => Text?.GetHashCode() ?? 0;

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => Text;
}