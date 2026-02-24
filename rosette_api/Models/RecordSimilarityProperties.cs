using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models
{
    public class RecordSimilarityProperties
    {
        /// <summary>
        /// Gets or sets the record similarity request's score threshold
        /// </summary>
        [JsonPropertyName("threshold")]
        public double? Threshold { get; set; } = 0.0;


        /// <summary>
        /// Gets or sets the record similarity request's include explain info parameter
        /// </summary>
        [JsonPropertyName("includeExplainInfo")]
        public bool? IncludeExplainInfo { get; set; }

        /// <summary>
        /// Gets or sets the record similarity request's parameters
        /// </summary>
        [JsonPropertyName("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Gets or sets the record similarity request's parameter universe
        /// </summary>
        [JsonPropertyName("parameterUniverse")]
        public string ParameterUniverse { get; set; }

        /// <summary>
        /// No-args constructor
        /// </summary>
        public RecordSimilarityProperties() { }

        /// <summary>
        /// Includes explain info constructor. Sets the threshold to 0.0.
        /// </summary>
        /// <param name="includeExplainInfo">The include explain info parameter</param>
        public RecordSimilarityProperties(bool includeExplainInfo)
        {
            this.IncludeExplainInfo = includeExplainInfo;
            this.Threshold = 0.0;
            this.Parameters = new Dictionary<string, string>();
            this.ParameterUniverse = "";
        }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="threshold">The score threshold</param>
        /// <param name="includeExplainInfo">The include explain info parameter</param>
        /// <param name="parameters">A map of string parameter names to string parameter values</param>
        /// <param name="parameterUniverse">The parameter universe to use</param>
        public RecordSimilarityProperties(double threshold, bool includeExplainInfo, Dictionary<string, string> parameters, string parameterUniverse)
        {
            this.Threshold = threshold;
            this.IncludeExplainInfo = includeExplainInfo;
            this.Parameters = parameters;
            this.ParameterUniverse = parameterUniverse;
        }

        /// <summary>
        /// Equals override
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is RecordSimilarityProperties)
            {
                RecordSimilarityProperties other = obj as RecordSimilarityProperties;
                List<bool> conditions = new List<bool>() {
                    this.Threshold == other.Threshold,
                    this.IncludeExplainInfo == other.IncludeExplainInfo,
                    this.Parameters != null && other.Parameters != null ?
                        Utilities.DictionaryEquals(this.Parameters, other.Parameters) : this.Parameters == other.Parameters,
                    this.ParameterUniverse == other.ParameterUniverse
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
            int h0 = this.Threshold.GetHashCode();
            int h1 = this.IncludeExplainInfo.GetHashCode();
            int h2 = this.Parameters != null ? this.Parameters.GetHashCode() : 1;
            int h3 = this.ParameterUniverse != null ? this.ParameterUniverse.GetHashCode() : 1;
            return h0 ^ h1 ^ h2 ^ h3;
        }

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns>This record similarity properties in JSON form</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
