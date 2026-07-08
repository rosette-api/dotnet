using Rosette.Api.Client.Models;
using Rosette.Api.Client.Models.JsonConverter;
using System.Text.Json;

namespace Rosette.Api.Tests.Models.JsonConverter;

/// <summary>
/// Tests for RecordSimilarityRecordsConverter class
/// </summary>
public class RecordSimilarityRecordsConverterTests
{
    private readonly JsonSerializerOptions _options;

    public RecordSimilarityRecordsConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new RecordSimilarityRecordsConverter());
        _options.Converters.Add(new RecordSimilarityFieldConverter());
        _options.Converters.Add(new UnfieldedRecordSimilarityConverter());
    }

    #region Write Tests - Basic Functionality

    [Fact]
    public void Write_EmptyRecords_SerializesCorrectly()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(),
            new List<Dictionary<string, RecordSimilarityField>>()
        );

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
        Assert.Contains("[]", json);
    }

    [Fact]
    public void Write_SingleRecord_SerializesCorrectly()
    {
        // Arrange
        var leftRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "John Smith" } },
                { "age", new NumberRecord(30) }
            }
        };
        var rightRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Jane Doe" } },
                { "age", new NumberRecord(28) }
            }
        };
        var records = new RecordSimilarityRecords(leftRecords, rightRecords);

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
        Assert.Contains("John Smith", json);
        Assert.Contains("Jane Doe", json);
        Assert.Contains("30", json);
        Assert.Contains("28", json);
    }

    [Fact]
    public void Write_MultipleRecords_SerializesAll()
    {
        // Arrange
        var leftRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Person 1" } }
            },
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Person 2" } }
            }
        };
        var rightRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Person 3" } }
            }
        };
        var records = new RecordSimilarityRecords(leftRecords, rightRecords);

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("Person 1", json);
        Assert.Contains("Person 2", json);
        Assert.Contains("Person 3", json);
    }

    #endregion

    #region Write Tests - Mixed Field Types

    [Fact]
    public void Write_FieldedAndUnfieldedRecords_SerializesBoth()
    {
        // Arrange
        var leftRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new FieldedNameRecord { Text = "John", Language = "eng" } },
                { "simpleText", new UnfieldedNameRecord { Text = "Simple" } },
                { "count", new NumberRecord(5) },
                { "active", new BooleanRecord(true) }
            }
        };
        var rightRecords = new List<Dictionary<string, RecordSimilarityField>>();
        var records = new RecordSimilarityRecords(leftRecords, rightRecords);

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("John", json);
        Assert.Contains("eng", json);
        Assert.Contains("Simple", json);
        Assert.Contains("5", json);
        Assert.Contains("true", json);
    }

    [Fact]
    public void Write_DatesAndAddresses_SerializesCorrectly()
    {
        // Arrange
        var leftRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "date", new UnfieldedDateRecord { Date = "2024-03-10" } },
                { "address", new UnfieldedAddressRecord { Address = "123 Main St" } },
                { "fieldedDate", new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" } }
            }
        };
        var rightRecords = new List<Dictionary<string, RecordSimilarityField>>();
        var records = new RecordSimilarityRecords(leftRecords, rightRecords);

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("2024-03-10", json);
        Assert.Contains("123 Main St", json);
        Assert.Contains("yyyy-MM-dd", json);
    }

    #endregion

    #region Write Tests - Complex Scenarios

    [Fact]
    public void Write_ComplexFieldedAddress_SerializesAllProperties()
    {
        // Arrange
        var leftRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                {
                    "address",
                    new FieldedAddressRecord
                    {
                        HouseNumber = "123",
                        Road = "Main Street",
                        City = "Springfield",
                        State = "IL",
                        Postcode = "62701",
                        Country = "USA"
                    }
                }
            }
        };
        var rightRecords = new List<Dictionary<string, RecordSimilarityField>>();
        var records = new RecordSimilarityRecords(leftRecords, rightRecords);

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("123", json);
        Assert.Contains("Main Street", json);
        Assert.Contains("Springfield", json);
        Assert.Contains("IL", json);
        Assert.Contains("62701", json);
        Assert.Contains("USA", json);
    }

    [Fact]
    public void Write_StringRecord_SerializesAsString()
    {
        // Arrange
        var leftRecords = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "text", new StringRecord { Text = "Sample Text" } }
            }
        };
        var rightRecords = new List<Dictionary<string, RecordSimilarityField>>();
        var records = new RecordSimilarityRecords(leftRecords, rightRecords);

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("Sample Text", json);
    }

    #endregion

    #region Read Tests

    [Fact]
    public void Read_EmptyRecords_DeserializesCorrectly()
    {
        // Arrange
        string json = "{\"left\":[],\"right\":[]}";

        // Act
        var result = JsonSerializer.Deserialize<RecordSimilarityRecords>(json, _options);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Left);
        Assert.Empty(result.Right);
    }

    [Fact]
    public void Read_AnyNonEmptyRecords_ThrowsNotSupported()
    {
        // Arrange - Any records with fields will fail because RecordSimilarityField deserialization is not supported
        string json = @"{
            ""left"": [
                {
                    ""age"": 30,
                    ""active"": true
                }
            ],
            ""right"": []
        }";

        // Act & Assert - Reading throws because RecordSimilarityField.Read is not supported
        Assert.Throws<NotSupportedException>(() =>
            JsonSerializer.Deserialize<RecordSimilarityRecords>(json, _options));
    }

    #endregion

    #region Round-Trip Tests

    [Fact]
    public void RoundTrip_SimpleRecords_SerializationWorks()
    {
        // Arrange - Note: Deserialization is not supported, only testing serialization
        var original = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>
            {
                new Dictionary<string, RecordSimilarityField>
                {
                    { "name", new UnfieldedNameRecord { Text = "John" } },
                    { "age", new NumberRecord(30) }
                }
            },
            new List<Dictionary<string, RecordSimilarityField>>
            {
                new Dictionary<string, RecordSimilarityField>
                {
                    { "name", new UnfieldedNameRecord { Text = "Jane" } },
                    { "age", new NumberRecord(28) }
                }
            }
        );

        // Act
        string json = JsonSerializer.Serialize(original, _options);

        // Assert - Verify JSON structure
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
        Assert.Contains("John", json);
        Assert.Contains("Jane", json);
        Assert.Contains("30", json);
        Assert.Contains("28", json);
    }

    [Fact]
    public void RoundTrip_EmptyRecords_SerializationWorks()
    {
        // Arrange - Note: Deserialization is not supported, only testing serialization
        var original = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(),
            new List<Dictionary<string, RecordSimilarityField>>()
        );

        // Act
        string json = JsonSerializer.Serialize(original, _options);

        // Assert - Verify JSON structure
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
        Assert.Contains("[]", json);
    }

    #endregion

    #region Validation Tests

    [Fact]
    public void Serialize_ProducesValidJson()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>
            {
                new Dictionary<string, RecordSimilarityField>
                {
                    { "field1", new UnfieldedNameRecord { Text = "Value1" } }
                }
            },
            new List<Dictionary<string, RecordSimilarityField>>()
        );

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert - Should be valid JSON
        var parsed = JsonDocument.Parse(json);
        Assert.NotNull(parsed);
        Assert.True(parsed.RootElement.TryGetProperty("left", out _));
        Assert.True(parsed.RootElement.TryGetProperty("right", out _));
    }

    [Fact]
    public void Write_HasCorrectStructure()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>
            {
                new Dictionary<string, RecordSimilarityField>
                {
                    { "name", new UnfieldedNameRecord { Text = "Test" } }
                }
            },
            new List<Dictionary<string, RecordSimilarityField>>()
        );

        // Act
        string json = JsonSerializer.Serialize(records, _options);
        var doc = JsonDocument.Parse(json);

        // Assert
        Assert.Equal(JsonValueKind.Object, doc.RootElement.ValueKind);
        Assert.Equal(JsonValueKind.Array, doc.RootElement.GetProperty("left").ValueKind);
        Assert.Equal(JsonValueKind.Array, doc.RootElement.GetProperty("right").ValueKind);
    }

    #endregion
}
