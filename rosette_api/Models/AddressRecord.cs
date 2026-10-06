using System.Text.Json;
using System.Text.Json.Serialization;
using Rosette.Api.Client.Models.JsonConverter;

namespace Rosette.Api.Client.Models;

/// <summary>
/// A record similarity field containing one or more addresses.
/// </summary>
[JsonConverter(typeof(AddressFieldConverter))]
public class AddressRecord : RecordSimilarityField
{
	/// <summary>Gets or sets the non-empty list of address values.</summary>
	[JsonPropertyName("data")]
	public List<AddressFieldData> Data { get; set; }

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="data">The address record</param>
	public AddressRecord(IEnumerable<AddressFieldData> data) => Data = FieldData.Copy(data);

	/// <summary>
	/// Equals override
	/// </summary>
	public override bool Equals(object? obj) => obj is AddressRecord other && FieldData.Equal(Data, other.Data);

	/// <summary>
	/// Hashcode override
	/// </summary>
	public override int GetHashCode() => FieldData.Hash(Data);

	/// <summary>
	/// ToString override
	/// </summary>
	public override string ToString() => JsonSerializer.Serialize(this);
}