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
            case FieldedNameRecord fieldedName:
                JsonSerializer.Serialize(writer, fieldedName, options);
                break;
            case FieldedDateRecord fieldedDate:
                JsonSerializer.Serialize(writer, fieldedDate, options);
                break;
            case FieldedAddressRecord fieldedAddress:
                JsonSerializer.Serialize(writer, fieldedAddress, options);
                break;
            // Unfielded records are handled by UnfieldedRecordSimilarityConverter
            case UnfieldedNameRecord or UnfieldedDateRecord or UnfieldedAddressRecord:
            case NumberRecord or BooleanRecord or StringRecord or UnknownFieldRecord:
                // Use the UnfieldedRecordSimilarityConverter for these types
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                break;
            default:
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                break;
        }
    }
}