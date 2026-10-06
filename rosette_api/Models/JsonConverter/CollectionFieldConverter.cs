using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rosette.Api.Client.Models.JsonConverter;

/// <summary>Shared scalar-or-array JSON behavior for record similarity fields.</summary>
public abstract class CollectionFieldConverter<TField, TElement> : JsonConverter<TField> where TField : class
{
    protected abstract List<TElement> GetData(TField field);
    protected abstract TField Create(List<TElement> data);

    public override TField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;
        var data = new List<TElement>();
        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in root.EnumerateArray())
            {
                data.Add(ReadElement(element, options));
            }
        }
        else
        {
            data.Add(ReadElement(root, options));
        }

        Validate(data);
        return Create(data);
    }

    protected virtual TElement ReadElement(JsonElement element, JsonSerializerOptions options)
    {
        if (element.ValueKind is JsonValueKind.Null or JsonValueKind.Array)
        {
            throw new JsonException("Field data cannot contain null values or nested arrays.");
        }
        return element.Deserialize<TElement>(options)
            ?? throw new JsonException("Field data cannot contain null values.");
    }

    protected virtual void WriteElement(Utf8JsonWriter writer, TElement value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, value, options);

    public override void Write(Utf8JsonWriter writer, TField value, JsonSerializerOptions options)
    {
        var data = GetData(value);
        Validate(data);
        if (data.Count == 1)
        {
            WriteElement(writer, data[0], options);
            return;
        }

        writer.WriteStartArray();
        foreach (var element in data)
        {
            WriteElement(writer, element, options);
        }
        writer.WriteEndArray();
    }

    private static void Validate(List<TElement>? data)
    {
        if (data is null || data.Count == 0 || data.Any(element => element is null))
        {
            throw new JsonException("Field data must be non-empty and contain no null elements.");
        }
    }
}






