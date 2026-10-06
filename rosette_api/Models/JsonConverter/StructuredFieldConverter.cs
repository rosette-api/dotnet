using System.Text.Json;

namespace Rosette.Api.Client.Models.JsonConverter;

/// <summary>Applies explicit fielded/unfielded dispatch within a collection wrapper.</summary>
public abstract class StructuredFieldConverter<TField, TData> : CollectionFieldConverter<TField, TData>
    where TField : class where TData : class
{
    private readonly RecordSimilarityFieldDataConverter<TData> _dataConverter = new();

    protected override TData ReadElement(JsonElement element, JsonSerializerOptions options) =>
        _dataConverter.ReadElement(element, options);

    protected override void WriteElement(Utf8JsonWriter writer, TData value, JsonSerializerOptions options) =>
        _dataConverter.Write(writer, value, options);
}
