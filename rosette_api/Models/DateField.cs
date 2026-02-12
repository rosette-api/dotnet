using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Abstract parent class for UnfieldedDate and FieldedDate
/// </summary>
public abstract class DateField : RecordSimilarityField
{
    /// <summary>
    /// Gets or sets the date field's date
    /// </summary>
    [JsonPropertyName("date")]
    public required string Date { get; set; }
}