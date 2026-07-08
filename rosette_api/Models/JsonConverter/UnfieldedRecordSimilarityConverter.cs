using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models.JsonConverter;

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
        return typeToConvert == typeof(UnfieldedNameRecord) ||
               typeToConvert == typeof(UnfieldedDateRecord) ||
               typeToConvert == typeof(UnfieldedAddressRecord) ||
               typeToConvert == typeof(NumberRecord) ||
               typeToConvert == typeof(BooleanRecord) ||
               typeToConvert == typeof(StringRecord) ||
               typeToConvert == typeof(UnknownFieldRecord) ||
               typeToConvert == typeof(object);
    }

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => new NumberRecord { Number = reader.GetDouble() },
            JsonTokenType.True or JsonTokenType.False => new BooleanRecord { Boolean = reader.GetBoolean() },
            JsonTokenType.String => typeToConvert switch
            {
                Type t when t == typeof(UnfieldedNameRecord) => new UnfieldedNameRecord { Text = reader.GetString() ?? string.Empty },
                Type t when t == typeof(UnfieldedDateRecord) => new UnfieldedDateRecord { Date = reader.GetString() ?? string.Empty },
                Type t when t == typeof(UnfieldedAddressRecord) => new UnfieldedAddressRecord { Address = reader.GetString() ?? string.Empty },
                Type t when t == typeof(StringRecord) => new StringRecord { Text = reader.GetString() ?? string.Empty },
                _ => reader.GetString()
            },
            JsonTokenType.StartObject => JsonSerializer.Deserialize<UnknownFieldRecord>(ref reader, options),
            _ => null
        };
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case UnfieldedNameRecord unfieldedName:
                writer.WriteStringValue(unfieldedName.Text);
                break;
            case UnfieldedDateRecord unfieldedDate:
                writer.WriteStringValue(unfieldedDate.Date);
                break;
            case UnfieldedAddressRecord unfieldedAddress:
                writer.WriteStringValue(unfieldedAddress.Address);
                break;
            case NumberRecord numberRecord:
                writer.WriteNumberValue(numberRecord.Number);
                break;
            case BooleanRecord booleanRecord:
                writer.WriteBooleanValue(booleanRecord.Boolean);
                break;
            case StringRecord stringRecord:
                writer.WriteStringValue(stringRecord.Text);
                break;
            case UnknownFieldRecord unknownField:
                if (unknownField.Data != null)
                {
                    unknownField.Data.WriteTo(writer, options);
                }
                else
                {
                    writer.WriteNullValue();
                }
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