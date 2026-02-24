using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Abstract parent class for UnfieldedName and FieldedName
/// </summary>
public abstract class NameField : RecordSimilarityField
{
    /// <summary>
    /// Gets or sets the name field's text
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}