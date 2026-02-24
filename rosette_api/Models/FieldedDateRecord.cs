using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing a fielded date
/// </summary>
public class FieldedDateRecord : DateField
{
    /// <summary>
    /// Gets or sets the date field's format
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public FieldedDateRecord() 
    {
        Date = string.Empty;
    }

    /// <summary>
    /// Full constructor
    /// </summary>
    /// <param name="date">The date in string format</param>
    /// <param name="format">The date's format. Rules are defined at https://docs.oracle.com/javase/8/docs/api/java/time/format/DateTimeFormatter.html</param>
    public FieldedDateRecord(string date, string? format = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(date);
        Date = date;
        Format = format;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if equal</returns>
    public override bool Equals(object? obj) =>
        obj is FieldedDateRecord other &&
        Date == other.Date &&
        Format == other.Format;

    /// <summary>
    /// Hashcode override
    /// </summary>
    /// <returns>The hashcode</returns>
    public override int GetHashCode() => HashCode.Combine(Date, Format);

    /// <summary>
    /// ToString override
    /// </summary>
    /// <returns>This fielded date in JSON form</returns>
    public override string ToString() => JsonSerializer.Serialize(this);
}