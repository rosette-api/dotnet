using Rosette.Api.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace rosette_api.Models.JsonConverter;

/// <summary>
/// JsonConverter for RecordSimilarityField interface polymorphic serialization
/// </summary>
public class RecordSimilarityFieldConverter : JsonConverter<RecordSimilarityField>
{
    public override RecordSimilarityField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Attempt to deserialize based on JSON structure
        return reader.TokenType switch
        {
            JsonTokenType.String => new StringRecord { Text = reader.GetString() ?? string.Empty },
            JsonTokenType.Number => new NumberRecord(reader.GetDouble()),
            JsonTokenType.True or JsonTokenType.False => new BooleanRecord(reader.GetBoolean()),
            JsonTokenType.StartObject => DeserializeObject(ref reader, options),
            _ => null
        };
    }

    private static RecordSimilarityField? DeserializeObject(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        // Check for known property patterns to determine type
        if (root.TryGetProperty("text", out _))
        {
            if (root.TryGetProperty("language", out _) ||
                root.TryGetProperty("entityType", out _))
            {
                return JsonSerializer.Deserialize<FieldedNameRecord>(root.GetRawText(), options);
            }
            return JsonSerializer.Deserialize<UnfieldedNameRecord>(root.GetRawText(), options);
        }

        if (root.TryGetProperty("date", out _))
        {
            if (root.TryGetProperty("format", out _))
            {
                return JsonSerializer.Deserialize<FieldedDateRecord>(root.GetRawText(), options);
            }
            return JsonSerializer.Deserialize<UnfieldedDateRecord>(root.GetRawText(), options);
        }

        if (root.TryGetProperty("address", out _))
        {
            return JsonSerializer.Deserialize<UnfieldedAddressRecord>(root.GetRawText(), options);
        }

        if (root.TryGetProperty("house", out _) || root.TryGetProperty("city", out _))
        {
            return JsonSerializer.Deserialize<FieldedAddressRecord>(root.GetRawText(), options);
        }

        // Default to unknown field
        return new UnknownFieldRecord(JsonNode.Parse(root.GetRawText()));
    }

    public override void Write(Utf8JsonWriter writer, RecordSimilarityField value, JsonSerializerOptions options)
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
            case FieldedNameRecord fieldedName:
                JsonSerializer.Serialize(writer, fieldedName, options);
                break;
            case FieldedDateRecord fieldedDate:
                JsonSerializer.Serialize(writer, fieldedDate, options);
                break;
            case FieldedAddressRecord fieldedAddress:
                JsonSerializer.Serialize(writer, fieldedAddress, options);
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
            default:
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                break;
        }
    }
}