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