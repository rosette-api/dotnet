using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models;

/// <summary>Base for individual name values; not a record similarity field.</summary>
public abstract class NameFieldData
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}



