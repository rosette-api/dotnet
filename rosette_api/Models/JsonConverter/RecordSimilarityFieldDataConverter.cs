using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models.JsonConverter;

/// <summary>Converts individual name, address, or date values inside collection wrappers.</summary>
public class RecordSimilarityFieldDataConverter<TData> : JsonConverter<TData> where TData : class
{
    public override TData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return ReadElement(document.RootElement, options);
    }

    public TData ReadElement(JsonElement element, JsonSerializerOptions options)
    {
        object? value = null;
        if (element.ValueKind == JsonValueKind.String)
        {
            var text = element.GetString()!;
            if (typeof(TData) == typeof(NameFieldData))
                value = new UnfieldedNameRecord { Text = text };
            else if (typeof(TData) == typeof(AddressFieldData))
                value = new UnfieldedAddressRecord { Address = text };
            else if (typeof(TData) == typeof(DateFieldData))
                value = new UnfieldedDateRecord { Date = text };
        }
        else if (element.ValueKind == JsonValueKind.Object)
        {
            if (typeof(TData) == typeof(NameFieldData))
                value = element.Deserialize<FieldedNameRecord>(options);
            else if (typeof(TData) == typeof(AddressFieldData))
                value = element.Deserialize<FieldedAddressRecord>(options);
            else if (typeof(TData) == typeof(DateFieldData))
                value = element.Deserialize<FieldedDateRecord>(options);
        }

        return value as TData ?? throw new JsonException("Field data must be a string or a supported fielded object.");
    }

    public override void Write(Utf8JsonWriter writer, TData value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case UnfieldedNameRecord name: writer.WriteStringValue(name.Text); break;
            case UnfieldedAddressRecord address: writer.WriteStringValue(address.Address); break;
            case UnfieldedDateRecord date: writer.WriteStringValue(date.Date); break;
            case FieldedNameRecord name: JsonSerializer.Serialize(writer, name, options); break;
            case FieldedAddressRecord address: JsonSerializer.Serialize(writer, address, options); break;
            case FieldedDateRecord date: JsonSerializer.Serialize(writer, date, options); break;
            default: throw new JsonException("Unsupported field data.");
        }
    }
}

