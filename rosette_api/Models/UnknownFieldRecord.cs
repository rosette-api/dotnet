using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing an unknown field
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class UnknownFieldRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets the unknown field's data
    /// </summary>
    [JsonPropertyName("data")]
    public JsonNode? Data { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The data as a JsonNode</param>
    public UnknownFieldRecord(JsonNode? data)
    {
        Data = data;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) =>
        obj is UnknownFieldRecord other && JsonNode.DeepEquals(Data, other.Data);

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => Data?.GetHashCode() ?? 0;

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => Data?.ToJsonString() ?? "null";
}