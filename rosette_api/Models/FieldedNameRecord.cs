using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary> 
/// Class for representing a fielded name
/// </summary>
public class FieldedNameRecord : NameField
{
    /// <summary>
    /// Gets or sets the language (ISO 639-3 code)
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Gets or sets the language of origin (ISO 639-3 code)
    /// </summary>
    [JsonPropertyName("languageOfOrigin")]
    public string? LanguageOfOrigin { get; set; }

    /// <summary>
    /// Gets or sets the script (ISO 15924 code for the name's script)
    /// </summary>
    [JsonPropertyName("script")]
    public string? Script { get; set; }

    /// <summary>
    /// Gets or sets the entity type (PERSON, LOCATION, or ORGANIZATION)
    /// </summary>
    [JsonPropertyName("entityType")]
    public string? EntityType { get; set; }

    /// <summary>
    /// No-args constructor
    /// </summary>
    public FieldedNameRecord() 
    {
        Text = string.Empty;
    }

    /// <summary>
    /// Full constructor
    /// </summary>
    /// <param name="text">Text describing the name</param>
    /// <param name="language">Language: ISO 639-3 code (optional)</param>
    /// <param name="languageOfOrigin">Language the name originates from: ISO 639-3 code (optional)</param>
    /// <param name="script">ISO 15924 code for the name's script (optional)</param>
    /// <param name="entityType">Entity type of the name: PERSON, LOCATION, or ORGANIZATION (optional)</param>
    public FieldedNameRecord(
        string text,
        string? language = null,
        string? languageOfOrigin = null,
        string? script = null,
        string? entityType = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        Text = text;
        Language = language;
        LanguageOfOrigin = languageOfOrigin;
        Script = script;
        EntityType = entityType;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if equal</returns>
    public override bool Equals(object? obj) =>
        obj is FieldedNameRecord other &&
        Text == other.Text &&
        Language == other.Language &&
        LanguageOfOrigin == other.LanguageOfOrigin &&
        Script == other.Script &&
        EntityType == other.EntityType;

    /// <summary>
    /// Hashcode override
    /// </summary>
    /// <returns>The hashcode</returns>
    public override int GetHashCode() =>
        HashCode.Combine(Text, Language, LanguageOfOrigin, Script, EntityType);

    /// <summary>
    /// ToString override
    /// </summary>
    /// <returns>This fielded name in JSON form</returns>
    public override string ToString() => JsonSerializer.Serialize(this);
}