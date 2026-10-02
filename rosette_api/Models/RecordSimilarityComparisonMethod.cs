using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models;

/// <summary>
/// The comparison methods for the Record Similarity endpoint
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<RecordSimilarityComparisonMethod>))]
public enum RecordSimilarityComparisonMethod
{
    [JsonStringEnumMemberName("one_to_one")]
    ONE_TO_ONE,
    [JsonStringEnumMemberName("one_to_n")]
    ONE_TO_N,
    [JsonStringEnumMemberName("n_to_m")]
    N_TO_M
}
