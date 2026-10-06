using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class RecordSimilarityRecordTests
{
    [Theory]
    [MemberData(nameof(Fields), MemberType = typeof(RecordSimilarityTestData))]
    public void Data_IsMutableAndReplaceable(Type type, string single, string multiple)
    {
        var field = Read(type, single);
        var replacement = Read(type, multiple);
        Data(field).Add(Data(replacement)[1]);
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse(multiple), JsonNode.Parse(Write(field))));
        Data(field).RemoveAt(1);
        Assert.Equal(single, Write(field));
        DataProperty(type).SetValue(field, Data(replacement));
        Assert.Equal(replacement, field);
    }

    [Theory]
    [MemberData(nameof(SingleFields), MemberType = typeof(RecordSimilarityTestData))]
    public void Constructor_RejectsNullAndEmptyCollections(Type type, string single)
    {
        var nullError = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, new object?[] { null }));
        Assert.IsType<ArgumentNullException>(nullError.InnerException);
        var empty = Data(Read(type, single));
        empty.Clear();
        var emptyError = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, new object[] { empty }));
        Assert.IsType<ArgumentException>(emptyError.InnerException);
    }

    [Theory]
    [MemberData(nameof(SingleFields), MemberType = typeof(RecordSimilarityTestData))]
    public void Serialization_RejectsEmptyAndNullDataAfterMutation(Type type, string single)
    {
        var field = Read(type, single);
        Data(field).Clear();
        Assert.Throws<JsonException>(() => Write(field));
        DataProperty(type).SetValue(field, null);
        Assert.Throws<JsonException>(() => Write(field));
    }

    [Theory]
    [InlineData(typeof(NameRecord), "\"Alice\"")]
    [InlineData(typeof(AddressRecord), "\"123 Main St\"")]
    [InlineData(typeof(DateRecord), "\"1993-04-16\"")]
    [InlineData(typeof(StringRecord), "\"manager\"")]
    public void NullElements_AreRejectedInConstructorAndAfterMutation(Type type, string single)
    {
        var field = Read(type, single);
        Data(field).Add(null);
        Assert.Throws<JsonException>(() => Write(field));
        var error = Assert.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, new object[] { Data(field) }));
        Assert.IsType<ArgumentException>(error.InnerException);
    }

    [Theory]
    [MemberData(nameof(SingleFields), MemberType = typeof(RecordSimilarityTestData))]
    public void Constructor_CopiesInput(Type type, string single)
    {
        var input = Data(Read(type, single));
        var field = Activator.CreateInstance(type, new object[] { input })!;
        input.Clear();
        Assert.Equal(single, Write(field));
    }
}
