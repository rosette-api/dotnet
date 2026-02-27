using rosette_api.Models.JsonConverter;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models;

[JsonConverter(typeof(RecordSimilarityRecordsConverter))]
public class RecordSimilarityRecords
{
    /// <summary>
    /// Gets or sets the the record similarity request's left records
    /// </summary>
    [JsonPropertyName("left")]
    public List<Dictionary<string, RecordSimilarityField>> Left { get; set; }

    /// <summary>
    /// Gets or sets the the record similarity request's right records
    /// </summary>
    [JsonPropertyName("right")]
    public List<Dictionary<string, RecordSimilarityField>> Right { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public RecordSimilarityRecords() { }

    /// <summary>
    /// Full constructor
    /// </summary>
    /// <param name="left">The left records</param>
    /// <param name="right">The right records</param>
    public RecordSimilarityRecords(List<Dictionary<string, RecordSimilarityField>> left, List<Dictionary<string, RecordSimilarityField>> right)
    {
        this.Left = left;
        this.Right = right;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if equal</returns>
    public override bool Equals(object obj)
    {
        if (obj is RecordSimilarityRecords)
        {
            RecordSimilarityRecords other = obj as RecordSimilarityRecords;
            List<bool> conditions = new List<bool>() {
                    this.Left != null && other.Left != null ? this.Left.SequenceEqual(other.Left) : this.Left == other.Left,
                    this.Right != null && other.Right != null ? this.Right.SequenceEqual(other.Right) : this.Right == other.Right
                };
            return conditions.All(condition => condition);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Hashcode override
    /// </summary>
    /// <returns>The hashcode</returns>
    public override int GetHashCode()
    {
        int h0 = this.Left != null ? this.Left.GetHashCode() : 1;
        int h1 = this.Right != null ? this.Right.GetHashCode() : 1;
        return h0 ^ h1;
    }

    /// <summary>
    /// ToString override.
    /// </summary>
    /// <returns>This record similarity records in JSON form</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
