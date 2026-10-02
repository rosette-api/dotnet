using System.Text.Json;
using System.Text.Json.Nodes;
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
        return typeToConvert == typeof(UnknownFieldRecord) ||
               typeToConvert == typeof(UnfieldedNameRecord) ||
               typeToConvert == typeof(UnfieldedAddressRecord) ||
               typeToConvert == typeof(UnfieldedDateRecord);
    }

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert == typeof(UnknownFieldRecord))
        {
            return new UnknownFieldRecord(JsonNode.Parse(ref reader));
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("An unfielded value must be a string.");
        }

        var text = reader.GetString()!;
        if (typeToConvert == typeof(UnfieldedNameRecord)) return new UnfieldedNameRecord { Text = text };
        if (typeToConvert == typeof(UnfieldedAddressRecord)) return new UnfieldedAddressRecord { Address = text };
        if (typeToConvert == typeof(UnfieldedDateRecord)) return new UnfieldedDateRecord { Date = text };
        throw new NotSupportedException($"Unsupported unfielded value type: {typeToConvert}.");
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case UnknownFieldRecord unknown:
                JsonSerializer.Serialize(writer, unknown.Data, options);
                break;
            case UnfieldedNameRecord name: writer.WriteStringValue(name.Text); break;
            case UnfieldedAddressRecord address: writer.WriteStringValue(address.Address); break;
            case UnfieldedDateRecord date: writer.WriteStringValue(date.Date); break;
            default: throw new JsonException("Unsupported unfielded value.");
        }
    }
}