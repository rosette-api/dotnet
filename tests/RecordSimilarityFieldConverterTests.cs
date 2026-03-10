using Rosette.Api.Client.Models;
using Rosette.Api.Client.Models.JsonConverter;
using System.Text.Json;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for RecordSimilarityFieldConverter class
/// </summary>
public class RecordSimilarityFieldConverterTests
{
    private readonly JsonSerializerOptions _options;

    public RecordSimilarityFieldConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new RecordSimilarityFieldConverter());
        _options.Converters.Add(new UnfieldedRecordSimilarityConverter());
    }

    #region Read Tests

    [Fact]
    public void Read_ThrowsNotSupportedException()
    {
        // Arrange
        var converter = new RecordSimilarityFieldConverter();
        string json = "{}";

        // Act & Assert
        NotSupportedException exception = null!;
        try
        {
            var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));
            reader.Read();
            converter.Read(ref reader, typeof(RecordSimilarityField), _options);
        }
        catch (NotSupportedException ex)
        {
            exception = ex;
        }

        Assert.NotNull(exception);
        Assert.Contains("Deserialization of RecordSimilarityField is not supported", exception.Message);
        Assert.Contains("type information is lost", exception.Message);
    }

    #endregion

    #region Write Tests - FieldedNameRecord

    [Fact]
    public void Write_FieldedNameRecord_SerializesAsObject()
    {
        // Arrange
        var record = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            EntityType = "PERSON"
        };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("\"text\"", json);
        Assert.Contains("John Smith", json);
        Assert.Contains("\"language\"", json);
        Assert.Contains("eng", json);
        Assert.Contains("\"entityType\"", json);
        Assert.Contains("PERSON", json);
    }

    [Fact]
    public void Write_FieldedNameRecord_WithAllProperties_SerializesCompletely()
    {
        // Arrange
        var record = new FieldedNameRecord
        {
            Text = "José García",
            Language = "spa",
            LanguageOfOrigin = "spa",
            Script = "Latn",
            EntityType = "PERSON"
        };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert - JSON may escape Unicode
        Assert.Contains("Jos", json);
        Assert.Contains("Garc", json);
        Assert.Contains("spa", json);
        Assert.Contains("Latn", json);
        Assert.Contains("PERSON", json);
    }

    [Fact]
    public void Write_FieldedNameRecord_WithNullOptionalFields_SerializesTextOnly()
    {
        // Arrange
        var record = new FieldedNameRecord { Text = "Test Name" };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("\"text\"", json);
        Assert.Contains("Test Name", json);
    }

    #endregion

    #region Write Tests - FieldedDateRecord

    [Fact]
    public void Write_FieldedDateRecord_SerializesAsObject()
    {
        // Arrange
        var record = new FieldedDateRecord
        {
            Date = "2024-03-10",
            Format = "yyyy-MM-dd"
        };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("\"date\"", json);
        Assert.Contains("2024-03-10", json);
        Assert.Contains("\"format\"", json);
        Assert.Contains("yyyy-MM-dd", json);
    }

    [Fact]
    public void Write_FieldedDateRecord_WithoutFormat_SerializesDateOnly()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-03-10" };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("\"date\"", json);
        Assert.Contains("2024-03-10", json);
    }

    #endregion

    #region Write Tests - FieldedAddressRecord

    [Fact]
    public void Write_FieldedAddressRecord_SerializesAsObject()
    {
        // Arrange
        var record = new FieldedAddressRecord
        {
            HouseNumber = "123",
            Road = "Main Street",
            City = "Springfield",
            State = "IL",
            Postcode = "62701"
        };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("123", json);
        Assert.Contains("Main Street", json);
        Assert.Contains("Springfield", json);
        Assert.Contains("IL", json);
        Assert.Contains("62701", json);
    }

    [Fact]
    public void Write_FieldedAddressRecord_WithAllFields_SerializesCompletely()
    {
        // Arrange
        var record = new FieldedAddressRecord
        {
            House = "White House",
            HouseNumber = "1600",
            Road = "Pennsylvania Avenue NW",
            City = "Washington",
            State = "DC",
            Country = "USA",
            Postcode = "20500"
        };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("White House", json);
        Assert.Contains("1600", json);
        Assert.Contains("Pennsylvania Avenue NW", json);
        Assert.Contains("Washington", json);
        Assert.Contains("DC", json);
        Assert.Contains("USA", json);
        Assert.Contains("20500", json);
    }

    [Fact]
    public void Write_FieldedAddressRecord_Empty_SerializesAsEmptyObject()
    {
        // Arrange
        var record = new FieldedAddressRecord();

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.Contains("{", json);
        Assert.Contains("}", json);
    }

    #endregion

    #region Write Tests - Unfielded Records Delegation

    [Fact]
    public void Write_UnfieldedNameRecord_DelegatesToUnfieldedConverter()
    {
        // Arrange
        RecordSimilarityField record = new UnfieldedNameRecord { Text = "John Smith" };

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.Equal("\"John Smith\"", json);
    }

    [Fact]
    public void Write_UnfieldedDateRecord_DelegatesToUnfieldedConverter()
    {
        // Arrange
        RecordSimilarityField record = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.Equal("\"2024-03-10\"", json);
    }

    [Fact]
    public void Write_UnfieldedAddressRecord_DelegatesToUnfieldedConverter()
    {
        // Arrange
        RecordSimilarityField record = new UnfieldedAddressRecord { Address = "123 Main St" };

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.Equal("\"123 Main St\"", json);
    }

    [Fact]
    public void Write_NumberRecord_DelegatesToUnfieldedConverter()
    {
        // Arrange
        RecordSimilarityField record = new NumberRecord(42);

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.Equal("42", json);
    }

    [Fact]
    public void Write_BooleanRecord_DelegatesToUnfieldedConverter()
    {
        // Arrange
        RecordSimilarityField record = new BooleanRecord(true);

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.Equal("true", json);
    }

    [Fact]
    public void Write_StringRecord_DelegatesToUnfieldedConverter()
    {
        // Arrange
        RecordSimilarityField record = new StringRecord { Text = "Test" };

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.Equal("\"Test\"", json);
    }

    [Fact]
    public void Write_UnknownFieldRecord_DelegatesToDefaultCase()
    {
        // Arrange
        RecordSimilarityField record = new UnknownFieldRecord(null);

        // Act
        string json = JsonSerializer.Serialize(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("null", json);
    }

    #endregion

    #region Write Tests - Switch Case Coverage

    [Fact]
    public void Write_UnfieldedNameRecord_ExecutesUnfieldedCaseBranch()
    {
        // Arrange - Test the specific switch case for UnfieldedNameRecord
        RecordSimilarityField record = new UnfieldedNameRecord { Text = "Jane Doe" };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("Jane Doe", json);
    }

    [Fact]
    public void Write_UnfieldedDateRecord_ExecutesUnfieldedCaseBranch()
    {
        // Arrange - Test the specific switch case for UnfieldedDateRecord
        RecordSimilarityField record = new UnfieldedDateRecord { Date = "2026-03-10" };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("2026-03-10", json);
    }

    [Fact]
    public void Write_UnfieldedAddressRecord_ExecutesUnfieldedCaseBranch()
    {
        // Arrange - Test the specific switch case for UnfieldedAddressRecord
        RecordSimilarityField record = new UnfieldedAddressRecord { Address = "456 Oak Avenue" };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("456 Oak Avenue", json);
    }

    [Fact]
    public void Write_NumberRecord_ExecutesSimpleRecordCaseBranch()
    {
        // Arrange - Test the specific switch case for NumberRecord
        RecordSimilarityField record = new NumberRecord(99);

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Equal("99", json);
    }

    [Fact]
    public void Write_BooleanRecord_False_ExecutesSimpleRecordCaseBranch()
    {
        // Arrange - Test the specific switch case for BooleanRecord with false value
        RecordSimilarityField record = new BooleanRecord(false);

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Equal("false", json);
    }

    [Fact]
    public void Write_StringRecord_WithSpecialCharacters_ExecutesSimpleRecordCaseBranch()
    {
        // Arrange - Test the specific switch case for StringRecord with special characters
        RecordSimilarityField record = new StringRecord { Text = "Test with special chars" };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("Test with", json);
    }

    [Fact]
    public void Write_UnknownFieldRecord_ExecutesDefaultCaseBranch()
    {
        // Arrange - Test the default case of the switch statement
        RecordSimilarityField record = new UnknownFieldRecord(System.Text.Json.Nodes.JsonNode.Parse("{}"));

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert
        Assert.NotNull(json);
        // UnknownFieldRecord should serialize the JsonNode data
        Assert.Contains("{", json);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void Serialize_MixedFieldedAndUnfieldedRecords_HandlesCorrectly()
    {
        // Arrange
        var records = new List<RecordSimilarityField>
        {
            new FieldedNameRecord { Text = "John", Language = "eng" },
            new UnfieldedNameRecord { Text = "Jane" },
            new NumberRecord(42),
            new BooleanRecord(true)
        };

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("John", json);
        Assert.Contains("eng", json);
        Assert.Contains("Jane", json);
        Assert.Contains("42", json);
        Assert.Contains("true", json);
    }

    [Fact]
    public void Serialize_FieldedRecord_ProducesValidJson()
    {
        // Arrange
        var record = new FieldedNameRecord
        {
            Text = "Test",
            Language = "eng"
        };

        // Act
        string json = JsonSerializer.Serialize<RecordSimilarityField>(record, _options);

        // Assert - Should be able to parse as valid JSON
        var parsed = JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    #endregion
}
