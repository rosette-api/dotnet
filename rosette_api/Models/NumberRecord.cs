using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models.JsonConverter;

namespace Rosette.Api.Client.Models;

/// <summary>
/// Class for representing a number record
/// </summary>
[JsonConverter(typeof(NumberFieldConverter))]
public class NumberRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets and sets the number record
    /// </summary>
    [JsonPropertyName("data")]
    public List<double> Data { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The number record</param>
    public NumberRecord(IEnumerable<double> data) => Data = FieldData.Copy(data);

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) => obj is NumberRecord other && FieldData.Equal(Data, other.Data);

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => FieldData.Hash(Data);

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}