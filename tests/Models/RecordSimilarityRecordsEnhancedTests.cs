using Rosette.Api.Client.Models;
using Rosette.Api.Client.Models.JsonConverter;
using System.Text.Json;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for RecordSimilarityRecords class
/// </summary>
public class RecordSimilarityRecordsEnhancedTests
{
    private readonly JsonSerializerOptions _options;

    public RecordSimilarityRecordsEnhancedTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new RecordSimilarityRecordsConverter());
        _options.Converters.Add(new RecordSimilarityFieldConverter());
        _options.Converters.Add(new UnfieldedRecordSimilarityConverter());
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_SetsDefaultValues()
    {
        // Act
        var records = new RecordSimilarityRecords();

        // Assert
        Assert.Null(records.Left);
        Assert.Null(records.Right);
    }

    [Fact]
    public void Constructor_WithEmptyLists_StoresEmptyLists()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>();
        var right = new List<Dictionary<string, RecordSimilarityField>>();

        // Act
        var records = new RecordSimilarityRecords(left, right);

        // Assert
        Assert.NotNull(records.Left);
        Assert.NotNull(records.Right);
        Assert.Empty(records.Left);
        Assert.Empty(records.Right);
    }

    [Fact]
    public void Constructor_WithData_StoresDataCorrectly()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "John" } }
            }
        };
        var right = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Jane" } }
            }
        };

        // Act
        var records = new RecordSimilarityRecords(left, right);

        // Assert
        Assert.Single(records.Left);
        Assert.Single(records.Right);
        Assert.Single(records.Left[0]);
        Assert.Single(records.Right[0]);
    }

    [Fact]
    public void Constructor_WithNullLists_AcceptsNulls()
    {
        // Act
        var records = new RecordSimilarityRecords(null, null);

        // Assert
        Assert.Null(records.Left);
        Assert.Null(records.Right);
    }

    [Fact]
    public void Constructor_WithMultipleRecords_StoresAll()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "field1", new UnfieldedNameRecord { Text = "Value1" } }
            },
            new Dictionary<string, RecordSimilarityField>
            {
                { "field2", new UnfieldedNameRecord { Text = "Value2" } }
            },
            new Dictionary<string, RecordSimilarityField>
            {
                { "field3", new UnfieldedNameRecord { Text = "Value3" } }
            }
        };
        var right = new List<Dictionary<string, RecordSimilarityField>>();

        // Act
        var records = new RecordSimilarityRecords(left, right);

        // Assert
        Assert.Equal(3, records.Left.Count);
        Assert.Empty(records.Right);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Left_CanBeModified_AfterConstruction()
    {
        // Arrange
        var records = new RecordSimilarityRecords();
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Test" } }
            }
        };

        // Act
        records.Left = left;

        // Assert
        Assert.NotNull(records.Left);
        Assert.Single(records.Left);
    }

    [Fact]
    public void Right_CanBeModified_AfterConstruction()
    {
        // Arrange
        var records = new RecordSimilarityRecords();
        var right = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Test" } }
            }
        };

        // Act
        records.Right = right;

        // Assert
        Assert.NotNull(records.Right);
        Assert.Single(records.Right);
    }

    [Fact]
    public void Left_And_Right_AreIndependent()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Left" } }
            }
        };
        var right = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Right" } }
            }
        };
        var records = new RecordSimilarityRecords(left, right);

        // Act - Modify left
        records.Left.Add(new Dictionary<string, RecordSimilarityField>
        {
            { "name", new UnfieldedNameRecord { Text = "Left2" } }
        });

        // Assert - Right unchanged
        Assert.Equal(2, records.Left.Count);
        Assert.Single(records.Right);
    }

    [Fact]
    public void Records_CanContainMultipleFields()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "John" } },
                { "age", new NumberRecord(30) },
                { "active", new BooleanRecord(true) },
                { "address", new UnfieldedAddressRecord { Address = "123 Main" } }
            }
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act & Assert
        Assert.Equal(4, records.Left[0].Count);
        Assert.IsType<UnfieldedNameRecord>(records.Left[0]["name"]);
        Assert.IsType<NumberRecord>(records.Left[0]["age"]);
        Assert.IsType<BooleanRecord>(records.Left[0]["active"]);
        Assert.IsType<UnfieldedAddressRecord>(records.Left[0]["address"]);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "John" } }
            }
        };
        var records1 = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());
        var records2 = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act & Assert
        Assert.True(records1.Equals(records2));
    }

    [Fact]
    public void Equals_DifferentLeftCount_ReturnsFalse()
    {
        // Arrange
        var left1 = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>()
        };
        var left2 = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>(),
            new Dictionary<string, RecordSimilarityField>()
        };
        var records1 = new RecordSimilarityRecords(left1, new List<Dictionary<string, RecordSimilarityField>>());
        var records2 = new RecordSimilarityRecords(left2, new List<Dictionary<string, RecordSimilarityField>>());

        // Act & Assert
        Assert.False(records1.Equals(records2));
    }

    [Fact]
    public void Equals_BothLeftNull_ReturnsTrue()
    {
        // Arrange
        var records1 = new RecordSimilarityRecords(null, new List<Dictionary<string, RecordSimilarityField>>());
        var records2 = new RecordSimilarityRecords(null, new List<Dictionary<string, RecordSimilarityField>>());

        // Act & Assert
        Assert.True(records1.Equals(records2));
    }

    [Fact]
    public void Equals_OneLeftNullOneNot_ReturnsFalse()
    {
        // Arrange
        var records1 = new RecordSimilarityRecords(null, new List<Dictionary<string, RecordSimilarityField>>());
        var records2 = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act & Assert
        Assert.False(records1.Equals(records2));
    }

    [Fact]
    public void Equals_BothRightNull_ReturnsTrue()
    {
        // Arrange
        var records1 = new RecordSimilarityRecords(new List<Dictionary<string, RecordSimilarityField>>(), null);
        var records2 = new RecordSimilarityRecords(new List<Dictionary<string, RecordSimilarityField>>(), null);

        // Act & Assert
        Assert.True(records1.Equals(records2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var records = new RecordSimilarityRecords();

        // Act & Assert
        Assert.False(records.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var records = new RecordSimilarityRecords();
        var differentType = "string";

        // Act & Assert
        Assert.False(records.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act & Assert
        Assert.True(records.Equals(records));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange - Use same list references for consistent hash
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>()
        };
        var right = new List<Dictionary<string, RecordSimilarityField>>();
        var records1 = new RecordSimilarityRecords(left, right);
        var records2 = new RecordSimilarityRecords(left, right);

        // Act
        int hash1 = records1.GetHashCode();
        int hash2 = records2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_NullValues_ReturnsConsistentValue()
    {
        // Arrange
        var records = new RecordSimilarityRecords(null, null);

        // Act
        int hash1 = records.GetHashCode();
        int hash2 = records.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_CalledMultipleTimes_ReturnsSameValue()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        int hash1 = records.GetHashCode();
        int hash2 = records.GetHashCode();
        int hash3 = records.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
        Assert.Equal(hash2, hash3);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_EmptyRecords_ReturnsValidJson()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = records.ToString();

        // Assert
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
        Assert.Contains("[]", json);
    }

    [Fact]
    public void ToString_WithRecords_ReturnsValidJson()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "John" } }
            }
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = records.ToString();

        // Assert
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
        Assert.Contains("John", json);
    }

    [Fact]
    public void ToString_ProducesValidParsableJson()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Test" } }
            }
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = records.ToString();

        // Assert - Should be valid JSON
        var parsed = JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    #endregion

    #region Serialization Tests

    [Fact]
    public void Serialization_EmptyRecords_ProducesValidJson()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("\"left\"", json);
        Assert.Contains("\"right\"", json);
    }

    [Fact]
    public void Serialization_WithFieldedRecords_SerializesCorrectly()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new FieldedNameRecord { Text = "John", Language = "eng" } }
            }
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("John", json);
        Assert.Contains("eng", json);
    }

    [Fact]
    public void Serialization_WithMixedFieldTypes_SerializesAll()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "John" } },
                { "age", new NumberRecord(30) },
                { "active", new BooleanRecord(true) },
                { "fieldedName", new FieldedNameRecord { Text = "Jane", Language = "eng" } }
            }
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("John", json);
        Assert.Contains("30", json);
        Assert.Contains("true", json);
        Assert.Contains("Jane", json);
        Assert.Contains("eng", json);
    }

    [Fact]
    public void Serialization_MultipleRecords_PreservesOrder()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "First" } }
            },
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Second" } }
            },
            new Dictionary<string, RecordSimilarityField>
            {
                { "name", new UnfieldedNameRecord { Text = "Third" } }
            }
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        string json = JsonSerializer.Serialize(records, _options);

        // Assert
        Assert.Contains("First", json);
        Assert.Contains("Second", json);
        Assert.Contains("Third", json);
        
        // Verify order
        int firstPos = json.IndexOf("First");
        int secondPos = json.IndexOf("Second");
        int thirdPos = json.IndexOf("Third");
        Assert.True(firstPos < secondPos);
        Assert.True(secondPos < thirdPos);
    }

    #endregion

    #region Collection Manipulation Tests

    [Fact]
    public void AddRecord_ToLeft_IncreasesCount()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        records.Left.Add(new Dictionary<string, RecordSimilarityField>
        {
            { "name", new UnfieldedNameRecord { Text = "New" } }
        });

        // Assert
        Assert.Single(records.Left);
    }

    [Fact]
    public void AddRecord_ToRight_IncreasesCount()
    {
        // Arrange
        var records = new RecordSimilarityRecords(
            new List<Dictionary<string, RecordSimilarityField>>(), 
            new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        records.Right.Add(new Dictionary<string, RecordSimilarityField>
        {
            { "name", new UnfieldedNameRecord { Text = "New" } }
        });

        // Assert
        Assert.Single(records.Right);
    }

    [Fact]
    public void ClearRecords_RemovesAllEntries()
    {
        // Arrange
        var left = new List<Dictionary<string, RecordSimilarityField>>
        {
            new Dictionary<string, RecordSimilarityField>(),
            new Dictionary<string, RecordSimilarityField>()
        };
        var records = new RecordSimilarityRecords(left, new List<Dictionary<string, RecordSimilarityField>>());

        // Act
        records.Left.Clear();

        // Assert
        Assert.Empty(records.Left);
    }

    #endregion
}
