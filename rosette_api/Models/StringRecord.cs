using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models.JsonConverter;

namespace Rosette.Api.Client.Models;

/// <summary>
/// Class for representing a string record
/// </summary>
[JsonConverter(typeof(StringFieldConverter))]
public class StringRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets and sets the string record
    /// </summary>
    [JsonPropertyName("data")]
    public List<string> Data { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The string record</param>
    public StringRecord(IEnumerable<string> data) => Data = FieldData.Copy(data);

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) => obj is StringRecord other && FieldData.Equal(Data, other.Data);

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => FieldData.Hash(Data);

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}