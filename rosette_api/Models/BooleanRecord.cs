using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models.JsonConverter;

namespace Rosette.Api.Client.Models;

/// <summary>
/// Class for representing a boolean record
/// </summary>
[JsonConverter(typeof(BooleanFieldConverter))]
public class BooleanRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets and sets the boolean record
    /// </summary>
    [JsonPropertyName("data")]
    public List<bool> Data { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The boolean record</param>
    public BooleanRecord(IEnumerable<bool> data) => Data = FieldData.Copy(data);

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) => obj is BooleanRecord other && FieldData.Equal(Data, other.Data);

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => FieldData.Hash(Data);

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}