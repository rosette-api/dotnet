using Rosette.Api.Client.Models;
using Rosette.Api.Client.Models.JsonConverter;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Rosette.Api.Tests.Models.JsonConverter;

/// <summary>
/// Tests for UnfieldedRecordSimilarityConverter class
/// </summary>
public class UnfieldedRecordSimilarityConverterTests
{
    private readonly JsonSerializerOptions _options;

    public UnfieldedRecordSimilarityConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new UnfieldedRecordSimilarityConverter());
    }

    #region CanConvert Tests

    [Fact]
    public void CanConvert_UnfieldedNameRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(UnfieldedNameRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_UnfieldedDateRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(UnfieldedDateRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_UnfieldedAddressRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(UnfieldedAddressRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_NumberRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(NumberRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_BooleanRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(BooleanRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_StringRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(StringRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_UnknownFieldRecord_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(UnknownFieldRecord));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_Object_ReturnsTrue()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(object));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanConvert_UnsupportedType_ReturnsFalse()
    {
        // Arrange
        var converter = new UnfieldedRecordSimilarityConverter();

        // Act
        bool result = converter.CanConvert(typeof(FieldedNameRecord));

        // Assert
        Assert.False(result);
    }

    #endregion

    #region Write Tests - UnfieldedNameRecord

    [Fact]
    public void Write_UnfieldedNameRecord_SerializesAsString()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "John Smith" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("\"John Smith\"", json);
    }

    [Fact]
    public void Write_UnfieldedNameRecord_WithSpecialCharacters_SerializesCorrectly()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "O'Brien \"Quote\"" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert - JSON escapes special characters
        Assert.Contains("Brien", json);
        Assert.Contains("Quote", json);
    }

    [Fact]
    public void Write_UnfieldedNameRecord_WithUnicode_SerializesCorrectly()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "José García" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert - JSON may escape Unicode characters
        Assert.Contains("Jos", json);
        Assert.Contains("Garc", json);
    }

    #endregion

    #region Write Tests - UnfieldedDateRecord

    [Fact]
    public void Write_UnfieldedDateRecord_SerializesAsString()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("\"2024-03-10\"", json);
    }

    [Fact]
    public void Write_UnfieldedDateRecord_DifferentFormat_SerializesAsProvided()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "March 10, 2024" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("\"March 10, 2024\"", json);
    }

    #endregion

    #region Write Tests - UnfieldedAddressRecord

    [Fact]
    public void Write_UnfieldedAddressRecord_SerializesAsString()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "123 Main St, Springfield, IL" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("\"123 Main St, Springfield, IL\"", json);
    }

    [Fact]
    public void Write_UnfieldedAddressRecord_WithNewlines_SerializesCorrectly()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "123 Main St\nSpringfield, IL\n62701" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Contains("123 Main St", json);
        Assert.Contains("Springfield", json);
    }

    #endregion

    #region Write Tests - NumberRecord

    [Fact]
    public void Write_NumberRecord_SerializesAsNumber()
    {
        // Arrange
        var record = new NumberRecord(42.5);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("42.5", json);
    }

    [Fact]
    public void Write_NumberRecord_Integer_SerializesWithoutDecimal()
    {
        // Arrange
        var record = new NumberRecord(100);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("100", json);
    }

    [Fact]
    public void Write_NumberRecord_NegativeNumber_SerializesCorrectly()
    {
        // Arrange
        var record = new NumberRecord(-17.3);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("-17.3", json);
    }

    [Fact]
    public void Write_NumberRecord_Zero_SerializesAsZero()
    {
        // Arrange
        var record = new NumberRecord(0);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("0", json);
    }

    #endregion

    #region Write Tests - BooleanRecord

    [Fact]
    public void Write_BooleanRecord_True_SerializesAsTrue()
    {
        // Arrange
        var record = new BooleanRecord(true);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("true", json);
    }

    [Fact]
    public void Write_BooleanRecord_False_SerializesAsFalse()
    {
        // Arrange
        var record = new BooleanRecord(false);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("false", json);
    }

    #endregion

    #region Write Tests - StringRecord

    [Fact]
    public void Write_StringRecord_SerializesAsString()
    {
        // Arrange
        var record = new StringRecord { Text = "Hello World" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("\"Hello World\"", json);
    }

    [Fact]
    public void Write_StringRecord_WithSpecialCharacters_SerializesCorrectly()
    {
        // Arrange
        var record = new StringRecord { Text = "Line 1\nLine 2\tTab" };

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Contains("Line 1", json);
        Assert.Contains("Line 2", json);
    }

    #endregion

    #region Write Tests - UnknownFieldRecord

    [Fact]
    public void Write_UnknownFieldRecord_WithJsonObject_SerializesAsObject()
    {
        // Arrange
        var jsonObject = new JsonObject
        {
            ["key1"] = "value1",
            ["key2"] = 42
        };
        var record = new UnknownFieldRecord(jsonObject);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Contains("key1", json);
        Assert.Contains("value1", json);
        Assert.Contains("key2", json);
        Assert.Contains("42", json);
    }

    [Fact]
    public void Write_UnknownFieldRecord_WithJsonArray_SerializesAsArray()
    {
        // Arrange
        var jsonArray = new JsonArray { "item1", "item2", 42 };
        var record = new UnknownFieldRecord(jsonArray);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Contains("item1", json);
        Assert.Contains("item2", json);
        Assert.Contains("42", json);
    }

    [Fact]
    public void Write_UnknownFieldRecord_WithNull_SerializesAsNull()
    {
        // Arrange
        var record = new UnknownFieldRecord(null);

        // Act
        string json = JsonSerializer.Serialize<object>(record, _options);

        // Assert
        Assert.Equal("null", json);
    }

    #endregion

    #region Write Tests - String Value

    [Fact]
    public void Write_StringValue_SerializesAsString()
    {
        // Arrange
        object value = "plain string";

        // Act
        string json = JsonSerializer.Serialize(value, _options);

        // Assert
        Assert.Equal("\"plain string\"", json);
    }

    #endregion

    #region Read Tests - NumberRecord

    [Fact]
    public void Read_NumberValue_CreatesNumberRecord()
    {
        // Arrange
        string json = "42.5";

        // Act
        var result = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<NumberRecord>(result);
        var numberRecord = (NumberRecord)result;
        Assert.Equal(42.5, numberRecord.Number);
    }

    [Fact]
    public void Read_IntegerValue_CreatesNumberRecord()
    {
        // Arrange
        string json = "100";

        // Act
        var result = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<NumberRecord>(result);
        var numberRecord = (NumberRecord)result;
        Assert.Equal(100, numberRecord.Number);
    }

    #endregion

    #region Read Tests - BooleanRecord

    [Fact]
    public void Read_TrueValue_CreatesBooleanRecord()
    {
        // Arrange
        string json = "true";

        // Act
        var result = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<BooleanRecord>(result);
        var booleanRecord = (BooleanRecord)result;
        Assert.True(booleanRecord.Boolean);
    }

    [Fact]
    public void Read_FalseValue_CreatesBooleanRecord()
    {
        // Arrange
        string json = "false";

        // Act
        var result = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<BooleanRecord>(result);
        var booleanRecord = (BooleanRecord)result;
        Assert.False(booleanRecord.Boolean);
    }

    #endregion

    #region Read Tests - String Values

    [Fact]
    public void Read_StringValue_ReturnsString()
    {
        // Arrange
        string json = "\"simple string\"";

        // Act
        var result = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<string>(result);
        Assert.Equal("simple string", result);
    }

    [Fact]
    public void Read_StringValue_WithSpecificType_CreatesCorrectRecord()
    {
        // Arrange
        string json = "\"123 Main St\"";

        // Act
        var result = JsonSerializer.Deserialize<UnfieldedAddressRecord>(json, _options);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("123 Main St", result.Address);
    }

    #endregion

    #region Round-Trip Tests

    [Fact]
    public void RoundTrip_NumberRecord_PreservesValue()
    {
        // Arrange
        var original = new NumberRecord(42.5);

        // Act
        string json = JsonSerializer.Serialize<object>(original, _options);
        var deserialized = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<NumberRecord>(deserialized);
        var result = (NumberRecord)deserialized;
        Assert.Equal(original.Number, result.Number);
    }

    [Fact]
    public void RoundTrip_BooleanRecord_PreservesValue()
    {
        // Arrange
        var original = new BooleanRecord(true);

        // Act
        string json = JsonSerializer.Serialize<object>(original, _options);
        var deserialized = JsonSerializer.Deserialize<object>(json, _options);

        // Assert
        Assert.IsType<BooleanRecord>(deserialized);
        var result = (BooleanRecord)deserialized;
        Assert.Equal(original.Boolean, result.Boolean);
    }

    #endregion
}
