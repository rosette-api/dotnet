using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// Class for representing an unfielded date
/// </summary>
[JsonConverter(typeof(UnfieldedRecordSimilarityConverter))]
public class UnfieldedDateRecord : DateField
{
    /// <summary>
    /// No-args constructor
    /// </summary>
    public UnfieldedDateRecord() 
    {
        Date = string.Empty;
    }

    /// <summary>
    /// Full constructor
    /// </summary>
    /// <param name="date">The date in string form</param>
    public UnfieldedDateRecord(string date)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(date);
        Date = date;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if equal</returns>
    public override bool Equals(object? obj) =>
        obj is UnfieldedDateRecord other && Date == other.Date;

    /// <summary>
    /// Hashcode override
    /// </summary>
    /// <returns>The hashcode</returns>
    public override int GetHashCode() => Date?.GetHashCode() ?? 0;

    /// <summary>
    /// ToString override. Also used for JSON serialization
    /// </summary>
    public override string ToString() => Date;
}