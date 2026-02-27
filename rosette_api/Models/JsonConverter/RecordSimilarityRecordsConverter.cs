using Rosette.Api.Client.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rosette_api.Models.JsonConverter;

/// <summary>
/// JsonConverter for RecordSimilarityRecords to properly serialize RecordSimilarityField values
/// </summary>
public class RecordSimilarityRecordsConverter : JsonConverter<RecordSimilarityRecords>
{
    private readonly RecordSimilarityFieldConverter _fieldConverter = new();

    public override RecordSimilarityRecords? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var left = ReadRecordList(root.GetProperty("left"), options);
        var right = ReadRecordList(root.GetProperty("right"), options);

        return new RecordSimilarityRecords(left, right);
    }

    private List<Dictionary<string, RecordSimilarityField>> ReadRecordList(
        JsonElement arrayElement,
        JsonSerializerOptions options)
    {
        var list = new List<Dictionary<string, RecordSimilarityField>>();

        foreach (var objElement in arrayElement.EnumerateArray())
        {
            var dict = new Dictionary<string, RecordSimilarityField>();

            foreach (var prop in objElement.EnumerateObject())
            {
                var reader = new Utf8JsonReader(
                    System.Text.Encoding.UTF8.GetBytes(prop.Value.GetRawText()));
                reader.Read();

                var field = _fieldConverter.Read(ref reader, typeof(RecordSimilarityField), options);
                if (field != null)
                {
                    dict[prop.Name] = field;
                }
            }

            list.Add(dict);
        }

        return list;
    }

    public override void Write(
        Utf8JsonWriter writer,
        RecordSimilarityRecords value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("left");
        WriteDictionaryList(writer, value.Left, options);

        writer.WritePropertyName("right");
        WriteDictionaryList(writer, value.Right, options);

        writer.WriteEndObject();
    }

    private void WriteDictionaryList(
        Utf8JsonWriter writer,
        List<Dictionary<string, RecordSimilarityField>> list,
        JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var dict in list)
        {
            writer.WriteStartObject();

            foreach (var kvp in dict)
            {
                writer.WritePropertyName(kvp.Key);
                _fieldConverter.Write(writer, kvp.Value, options);
            }

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }
}