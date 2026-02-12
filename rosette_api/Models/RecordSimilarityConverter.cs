using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Models;

/// <summary>
/// JsonConverter for Unfielded Record Similarity objects
/// </summary>
public class UnfieldedRecordSimilarityConverter : JsonConverter<object>
{
    /// <summary>
    /// Initializes a new instance of the UnfieldedRecordSimilarityConverter class
    /// </summary>
    public UnfieldedRecordSimilarityConverter()
    {
        // Default constructor for JSON serialization
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(UnknownFieldRecord) ||
               typeToConvert == typeof(NumberRecord) ||
               typeToConvert == typeof(BooleanRecord) ||
               typeToConvert == typeof(object);
    }

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => new NumberRecord { Number = reader.GetDouble() },
            JsonTokenType.True or JsonTokenType.False => new BooleanRecord { Boolean = reader.GetBoolean() },
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.StartObject => JsonSerializer.Deserialize<UnknownFieldRecord>(ref reader, options),
            _ => null
        };
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case UnknownFieldRecord unknownField:
                JsonSerializer.Serialize(writer, unknownField.Data, options);
                break;
            case NumberRecord numberRecord:
                writer.WriteNumberValue(numberRecord.Number);
                break;
            case BooleanRecord booleanRecord:
                writer.WriteBooleanValue(booleanRecord.Boolean);
                break;
            case string stringValue:
                writer.WriteStringValue(stringValue);
                break;
            default:
                writer.WriteStringValue(value?.ToString() ?? string.Empty);
                break;
        }
    }
}