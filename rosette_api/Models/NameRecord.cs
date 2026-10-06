using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models.JsonConverter;

namespace Rosette.Api.Client.Models;

/// <summary>
/// A record similarity field containing one or more names.
/// </summary>
[JsonConverter(typeof(NameRecordConverter))]
public class NameRecord : RecordSimilarityField
{
    /// <summary>
    /// Gets or sets the non-empty list of name values.
    /// </summary>
    [JsonPropertyName("data")]
    public List<NameFieldData> Data { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data">The name record</param>
    public NameRecord(IEnumerable<NameFieldData> data) => Data = FieldData.Copy(data);

    /// <summary>
    /// Equals override
    /// </summary>
    public override bool Equals(object? obj) => obj is NameRecord other && FieldData.Equal(Data, other.Data);

    /// <summary>
    /// Hashcode override
    /// </summary>
    public override int GetHashCode() => FieldData.Hash(Data);

    /// <summary>
    /// ToString override
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}