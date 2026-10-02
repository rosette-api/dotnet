using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models.JsonConverter;

namespace Rosette.Api.Client.Models;

/// <summary>
/// A record similarity field containing one or more dates.
/// </summary>
[JsonConverter(typeof(DateFieldConverter))]
public class DateRecord : RecordSimilarityField
{
    /// <summary>Gets or sets the non-empty list of date values.</summary>
    [JsonPropertyName("data")]
    public List<DateFieldData> Data { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The date record</param>
    public DateRecord(IEnumerable<DateFieldData> data) => Data = FieldData.Copy(data);

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) => obj is DateRecord other && FieldData.Equal(Data, other.Data);

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => FieldData.Hash(Data);

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}