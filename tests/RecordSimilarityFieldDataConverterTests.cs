using System.Text.Json;
using Rosette.Api.Client.Models.JsonConverter;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class RecordSimilarityFieldDataConverterTests
{
    [Theory]
    [MemberData(nameof(FieldDataValues), MemberType = typeof(RecordSimilarityTestData))]
    public void FieldDataConverter_CanBeExplicitlyUsedWithoutModelAttributes(
        Type dataType, Type fieldedType, Type unfieldedType, string scalarJson, string objectJson)
    {
        var options = new JsonSerializerOptions(IgnoreNullOptions);
        var converterType = typeof(RecordSimilarityFieldDataConverter<>).MakeGenericType(dataType);
        options.Converters.Add((System.Text.Json.Serialization.JsonConverter)Activator.CreateInstance(converterType)!);
        var scalar = JsonSerializer.Deserialize(scalarJson, dataType, options)!;
        var fielded = JsonSerializer.Deserialize(objectJson, dataType, options)!;
        Assert.IsType(unfieldedType, scalar);
        Assert.IsType(fieldedType, fielded);
        Assert.Equal(scalarJson, JsonSerializer.Serialize(scalar, dataType, options));
        Assert.Equal(objectJson, JsonSerializer.Serialize(fielded, dataType, options));
        Assert.Null(JsonSerializer.Deserialize("null", dataType, options));
        Assert.Equal("null", JsonSerializer.Serialize((object?)null, dataType, options));
    }
}
