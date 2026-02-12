using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models {
    /// <summary>RecordFieldType
    /// <para>
    /// The possible record types that can be used in the Record Similarity endpoint
    /// </para>
    /// </summary>
    public static class RecordFieldType
    {
        public const string RniName = "rni_name";
        public const string RniDate = "rni_date";
        public const string RniAddress = "rni_address";
        public const string RniString = "rni_string";
        public const string RniNumber = "rni_number";
        public const string RniBoolean = "rni_boolean";
    }

    /// <summary>
    /// Class for representing record similarity field information
    /// </summary>
    public class RecordSimilarityFieldInfo
    {
        /// <summary>
        /// Gets or sets the record's field type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the record's field weight
        /// </summary>
        [JsonPropertyName("weight")]
        public double? Weight { get; set; }

        /// <summary>
        /// Gets or sets the record's field scoreIfNull
        /// </summary>
        [JsonPropertyName("scoreIfNull")]
        public double? ScoreIfNull { get; set; }

        /// <summary>
        /// No-args constructor
        /// </summary>
        public RecordSimilarityFieldInfo() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="type">The record's field type</param>
        /// <param name="weight">The record's field weight</param>
        /// <param name="scoreIfNull">The record's field scoreIfNull</param>
        public RecordSimilarityFieldInfo(string type, double? weight, double? scoreIfNull)
        {
            this.Type = type;
            this.Weight = weight;
            this.ScoreIfNull = scoreIfNull;
        }


        /// <summary>
        /// Equals override
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is RecordSimilarityFieldInfo)
            {
                RecordSimilarityFieldInfo other = obj as RecordSimilarityFieldInfo;
                List<bool> conditions = new List<bool>() {
                    this.Type == other.Type,
                    this.Weight == other.Weight,
                    this.ScoreIfNull == other.ScoreIfNull
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
            int h0 = this.Type.GetHashCode();
            int h1 = this.Weight != null ? this.Weight.GetHashCode() : 1;
            int h2 = this.ScoreIfNull != null ? this.ScoreIfNull.GetHashCode() : 1;
            return h0 ^ h1 ^ h2;
        }

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns>This record similarity field info in JSON form</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}