using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models.JsonConverter;

/// <summary>
/// JsonConverter for RecordSimilarityField interface polymorphic serialization
/// </summary>
public class RecordSimilarityFieldConverter : JsonConverter<RecordSimilarityField>
{
    public override RecordSimilarityField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException(
            "Deserialization of RecordSimilarityField is not supported. " +
            "Unfielded records cannot be reliably reconstructed from JSON responses " +
            "because type information is lost. Please work with the raw JSON response data instead.");
    }

    public override void Write(Utf8JsonWriter writer, RecordSimilarityField value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case NameRecord nameRecord:
                JsonSerializer.Serialize(writer, nameRecord, options);
                break;
            case AddressRecord addressRecord:
                JsonSerializer.Serialize(writer, addressRecord, options);
                break;
            case DateRecord dateRecord:
                JsonSerializer.Serialize(writer, dateRecord, options);
                break;
            case NumberRecord numberRecord:
                JsonSerializer.Serialize(writer, numberRecord, options);
                break;
            case BooleanRecord booleanRecord:
                JsonSerializer.Serialize(writer, booleanRecord, options);
                break;
            case StringRecord stringRecord:
                JsonSerializer.Serialize(writer, stringRecord, options);
                break;
            case UnknownFieldRecord unknownField:
                JsonSerializer.Serialize(writer, unknownField, options);
                break;
            default:
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                break;
        }
    }
}