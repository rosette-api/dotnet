using Rosette.Api.Client.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for UnknownFieldRecord class
/// </summary>
public class UnknownFieldRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_WithNull_CreatesRecordWithNullData()
    {
        // Act
        var record = new UnknownFieldRecord(null);

        // Assert
        Assert.Null(record.Data);
    }

    [Fact]
    public void Constructor_WithJsonObject_SetsDataProperty()
    {
        // Arrange
        var jsonObject = new JsonObject
        {
            ["key"] = "value",
            ["number"] = 42
        };

        // Act
        var record = new UnknownFieldRecord(jsonObject);

        // Assert
        Assert.NotNull(record.Data);
        Assert.Equal("value", record.Data["key"]?.GetValue<string>());
        Assert.Equal(42, record.Data["number"]?.GetValue<int>());
    }

    [Fact]
    public void Constructor_WithJsonArray_SetsDataProperty()
    {
        // Arrange
        var jsonArray = new JsonArray { "item1", "item2", "item3" };

        // Act
        var record = new UnknownFieldRecord(jsonArray);

        // Assert
        Assert.NotNull(record.Data);
        Assert.Equal(3, record.Data.AsArray().Count);
    }

    [Fact]
    public void Constructor_WithJsonValue_SetsDataProperty()
    {
        // Arrange
        var jsonValue = JsonValue.Create("simple string");

        // Act
        var record = new UnknownFieldRecord(jsonValue);

        // Assert
        Assert.NotNull(record.Data);
        Assert.Equal("simple string", record.Data.GetValue<string>());
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Data_IsReadOnly()
    {
        // Arrange
        var jsonObject = new JsonObject { ["key"] = "value" };
        var record = new UnknownFieldRecord(jsonObject);

        // Assert - Data property should be get-only
        Assert.NotNull(record.Data);
        // Attempting to set Data would not compile
    }

    [Fact]
    public void Data_PreservesComplexStructure()
    {
        // Arrange
        var jsonObject = new JsonObject
        {
            ["name"] = "Test",
            ["nested"] = new JsonObject
            {
                ["field1"] = "value1",
                ["field2"] = 123
            },
            ["array"] = new JsonArray { 1, 2, 3 }
        };

        // Act
        var record = new UnknownFieldRecord(jsonObject);

        // Assert
        Assert.NotNull(record.Data);
        Assert.Equal("Test", record.Data["name"]?.GetValue<string>());
        Assert.Equal("value1", record.Data["nested"]?["field1"]?.GetValue<string>());
        Assert.Equal(3, record.Data["array"]?.AsArray().Count);
    }

    [Fact]
    public void Data_HandlesEmptyObject()
    {
        // Arrange
        var jsonObject = new JsonObject();

        // Act
        var record = new UnknownFieldRecord(jsonObject);

        // Assert
        Assert.NotNull(record.Data);
        Assert.Empty(record.Data.AsObject());
    }

    [Fact]
    public void Data_HandlesEmptyArray()
    {
        // Arrange
        var jsonArray = new JsonArray();

        // Act
        var record = new UnknownFieldRecord(jsonArray);

        // Assert
        Assert.NotNull(record.Data);
        Assert.Empty(record.Data.AsArray());
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_BothNull_ReturnsTrue()
    {
        // Arrange
        var record1 = new UnknownFieldRecord(null);
        var record2 = new UnknownFieldRecord(null);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_SameJsonObject_ReturnsTrue()
    {
        // Arrange
        var jsonObject1 = new JsonObject { ["key"] = "value" };
        var jsonObject2 = new JsonObject { ["key"] = "value" };
        var record1 = new UnknownFieldRecord(jsonObject1);
        var record2 = new UnknownFieldRecord(jsonObject2);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentJsonObject_ReturnsFalse()
    {
        // Arrange
        var jsonObject1 = new JsonObject { ["key"] = "value1" };
        var jsonObject2 = new JsonObject { ["key"] = "value2" };
        var record1 = new UnknownFieldRecord(jsonObject1);
        var record2 = new UnknownFieldRecord(jsonObject2);

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_SameJsonArray_ReturnsTrue()
    {
        // Arrange
        var jsonArray1 = new JsonArray { 1, 2, 3 };
        var jsonArray2 = new JsonArray { 1, 2, 3 };
        var record1 = new UnknownFieldRecord(jsonArray1);
        var record2 = new UnknownFieldRecord(jsonArray2);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentJsonArray_ReturnsFalse()
    {
        // Arrange
        var jsonArray1 = new JsonArray { 1, 2, 3 };
        var jsonArray2 = new JsonArray { 4, 5, 6 };
        var record1 = new UnknownFieldRecord(jsonArray1);
        var record2 = new UnknownFieldRecord(jsonArray2);

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_OneNullOneWithData_ReturnsFalse()
    {
        // Arrange
        var record1 = new UnknownFieldRecord(null);
        var record2 = new UnknownFieldRecord(new JsonObject { ["key"] = "value" });

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new UnknownFieldRecord(new JsonObject { ["key"] = "value" });

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new UnknownFieldRecord(new JsonObject { ["key"] = "value" });
        var differentType = "different";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new UnknownFieldRecord(new JsonObject { ["key"] = "value" });

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    [Fact]
    public void Equals_NestedStructure_ComparesCorrectly()
    {
        // Arrange
        var json1 = new JsonObject
        {
            ["nested"] = new JsonObject { ["value"] = 123 }
        };
        var json2 = new JsonObject
        {
            ["nested"] = new JsonObject { ["value"] = 123 }
        };
        var record1 = new UnknownFieldRecord(json1);
        var record2 = new UnknownFieldRecord(json2);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameData_ReturnsSameHashCode()
    {
        // Arrange - use same object reference
        var jsonObject = new JsonObject { ["key"] = "value" };
        var record1 = new UnknownFieldRecord(jsonObject);
        var record2 = new UnknownFieldRecord(jsonObject);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentData_ReturnsDifferentHashCode()
    {
        // Arrange
        var jsonObject1 = new JsonObject { ["key"] = "value1" };
        var jsonObject2 = new JsonObject { ["key"] = "value2" };
        var record1 = new UnknownFieldRecord(jsonObject1);
        var record2 = new UnknownFieldRecord(jsonObject2);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_NullData_ReturnsZero()
    {
        // Arrange
        var record = new UnknownFieldRecord(null);

        // Act
        int hash = record.GetHashCode();

        // Assert
        Assert.Equal(0, hash);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_Null_ReturnsNull()
    {
        // Arrange
        var record = new UnknownFieldRecord(null);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("null", result);
    }

    [Fact]
    public void ToString_JsonObject_ReturnsJsonString()
    {
        // Arrange
        var jsonObject = new JsonObject { ["key"] = "value" };
        var record = new UnknownFieldRecord(jsonObject);

        // Act
        string result = record.ToString();

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains("key", result);
        Assert.Contains("value", result);
    }

    [Fact]
    public void ToString_JsonArray_ReturnsJsonString()
    {
        // Arrange
        var jsonArray = new JsonArray { 1, 2, 3 };
        var record = new UnknownFieldRecord(jsonArray);

        // Act
        string result = record.ToString();

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains("1", result);
        Assert.Contains("2", result);
        Assert.Contains("3", result);
    }

    [Fact]
    public void ToString_ProducesValidJson()
    {
        // Arrange
        var jsonObject = new JsonObject
        {
            ["name"] = "Test",
            ["value"] = 42
        };
        var record = new UnknownFieldRecord(jsonObject);

        // Act
        string result = record.ToString();

        // Assert - Should be able to parse it back
        var parsed = JsonNode.Parse(result);
        Assert.NotNull(parsed);
        Assert.Equal("Test", parsed["name"]?.GetValue<string>());
        Assert.Equal(42, parsed["value"]?.GetValue<int>());
    }

    [Fact]
    public void ToString_EmptyObject_ReturnsEmptyJsonObject()
    {
        // Arrange
        var jsonObject = new JsonObject();
        var record = new UnknownFieldRecord(jsonObject);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("{}", result);
    }

    [Fact]
    public void ToString_EmptyArray_ReturnsEmptyJsonArray()
    {
        // Arrange
        var jsonArray = new JsonArray();
        var record = new UnknownFieldRecord(jsonArray);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("[]", result);
    }

    #endregion
}
