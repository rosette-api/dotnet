using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models;

/// <summary>Base for individual date values; not a record similarity field.</summary>
public abstract class DateFieldData
{
    [JsonPropertyName("date")]
    public required string Date { get; set; }
}



