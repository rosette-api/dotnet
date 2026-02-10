using System.Text.Json.Serialization;

namespace Rosette.Api;

public class Name
{
    [JsonPropertyName("text")]
    public string Text { get; private set; }

    [JsonPropertyName("entityType")]
    public string? EntityType { get; private set; }

    [JsonPropertyName("language")]
    public string? Language { get; private set; }

    [JsonPropertyName("script")]
    public string? Script { get; private set; }

    /// <summary>
    /// Constructor for a Name object, used by several endpoints
    /// </summary>
    /// <param name="text">required text</param>
    /// <param name="entityType">optional entity type</param>
    /// <param name="language">optional language code</param>
    /// <param name="script">optional script code</param>
    [JsonConstructor]
    public Name(string text, string? entityType = null, string? language = null, string? script = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        Text = text;
        EntityType = entityType;
        Language = language;
        Script = script;
    }

    /// <summary>
    /// SetEntityType sets the optional entity type. PERSON, LOCATION and ORGANIZATION
    /// are currently supported.
    /// </summary>
    /// <param name="type">entity type, PERSON, LOCATION or ORGANIZATION</param>
    /// <returns>updated RosetteName object</returns>
    public Name SetEntityType(string type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(type);

        string[] validTypes = ["PERSON", "LOCATION", "ORGANIZATION"];
        if (!validTypes.Contains(type, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                $"Entity type must be one of: {string.Join(", ", validTypes)}. Provided: {type}",
                nameof(type));
        }

        EntityType = type.ToUpperInvariant();
        return this;
    }

    /// <summary>
    /// SetLanguage sets the optional ISO-639-3 language code for the name's language
    /// </summary>
    /// <param name="language">ISO-639-3 language code</param>
    /// <returns>updated RosetteName object</returns>
    public Name SetLanguage(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        Language = language;
        return this;
    }

    /// <summary>
    /// SetScript sets the ISO-15924 code for the name's script
    /// </summary>
    /// <param name="script">ISO-15924 script code</param>
    /// <returns>updated RosetteName object</returns>
    public Name SetScript(string script)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(script);
        Script = script;
        return this;
    }
}