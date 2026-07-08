using Rosette.Api.Client;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for the Utilities class extension methods
/// </summary>
public class UtilitiesTests
{
    #region DictionaryEquals - Null Handling Tests

    [Fact]
    public void DictionaryEquals_BothNull_ReturnsTrue()
    {
        // Arrange
        Dictionary<string, string>? first = null;
        Dictionary<string, string>? second = null;

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_FirstNull_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string>? first = null;
        Dictionary<string, string> second = new() { { "key", "value" } };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DictionaryEquals_SecondNull_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string> first = new() { { "key", "value" } };
        Dictionary<string, string>? second = null;

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DictionaryEquals - Empty Dictionary Tests

    [Fact]
    public void DictionaryEquals_BothEmpty_ReturnsTrue()
    {
        // Arrange
        Dictionary<string, string> first = new();
        Dictionary<string, string> second = new();

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_OneEmptyOneNot_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string> first = new();
        Dictionary<string, string> second = new() { { "key", "value" } };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DictionaryEquals - Count Tests

    [Fact]
    public void DictionaryEquals_DifferentCounts_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string> first = new() 
        { 
            { "key1", "value1" } 
        };
        Dictionary<string, string> second = new() 
        { 
            { "key1", "value1" }, 
            { "key2", "value2" } 
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DictionaryEquals - Content Comparison Tests

    [Fact]
    public void DictionaryEquals_SameContent_ReturnsTrue()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "name", "John" },
            { "age", "30" },
            { "city", "Boston" }
        };
        Dictionary<string, string> second = new()
        {
            { "name", "John" },
            { "age", "30" },
            { "city", "Boston" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_SameContentDifferentOrder_ReturnsTrue()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "name", "John" },
            { "age", "30" },
            { "city", "Boston" }
        };
        Dictionary<string, string> second = new()
        {
            { "city", "Boston" },
            { "name", "John" },
            { "age", "30" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_DifferentKeys_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };
        Dictionary<string, string> second = new()
        {
            { "key1", "value1" },
            { "key3", "value2" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DictionaryEquals_DifferentValues_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };
        Dictionary<string, string> second = new()
        {
            { "key1", "value1" },
            { "key2", "different" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DictionaryEquals_KeyNotInSecond_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "key1", "value1" },
            { "uniqueKey", "value2" }
        };
        Dictionary<string, string> second = new()
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DictionaryEquals - Generic Type Tests

    [Fact]
    public void DictionaryEquals_IntKeys_WorksCorrectly()
    {
        // Arrange
        Dictionary<int, string> first = new()
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" }
        };
        Dictionary<int, string> second = new()
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_IntValues_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, int> first = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }
        };
        Dictionary<string, int> second = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_IntValuesDifferent_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, int> first = new()
        {
            { "one", 1 },
            { "two", 2 }
        };
        Dictionary<string, int> second = new()
        {
            { "one", 1 },
            { "two", 999 }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DictionaryEquals_DoubleValues_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, double> first = new()
        {
            { "pi", 3.14159 },
            { "e", 2.71828 }
        };
        Dictionary<string, double> second = new()
        {
            { "pi", 3.14159 },
            { "e", 2.71828 }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_BooleanValues_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, bool> first = new()
        {
            { "isActive", true },
            { "isDeleted", false }
        };
        Dictionary<string, bool> second = new()
        {
            { "isActive", true },
            { "isDeleted", false }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    #endregion

    #region DictionaryEquals - Complex Object Tests

    [Fact]
    public void DictionaryEquals_ObjectValues_UsesObjectEquals()
    {
        // Arrange
        var obj1 = new TestObject { Id = 1, Name = "Test" };
        var obj2 = new TestObject { Id = 1, Name = "Test" };

        Dictionary<string, TestObject> first = new()
        {
            { "key1", obj1 }
        };
        Dictionary<string, TestObject> second = new()
        {
            { "key1", obj2 }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_ObjectValuesDifferent_ReturnsFalse()
    {
        // Arrange
        var obj1 = new TestObject { Id = 1, Name = "Test" };
        var obj2 = new TestObject { Id = 2, Name = "Different" };

        Dictionary<string, TestObject> first = new()
        {
            { "key1", obj1 }
        };
        Dictionary<string, TestObject> second = new()
        {
            { "key1", obj2 }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region DictionaryEquals - Special Cases

    [Fact]
    public void DictionaryEquals_EmptyStringKeys_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "", "empty key value" },
            { "normal", "value" }
        };
        Dictionary<string, string> second = new()
        {
            { "", "empty key value" },
            { "normal", "value" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_EmptyStringValues_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, string> first = new()
        {
            { "key1", "" },
            { "key2", "value" }
        };
        Dictionary<string, string> second = new()
        {
            { "key1", "" },
            { "key2", "value" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_NullStringValue_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, string?> first = new()
        {
            { "key1", null },
            { "key2", "value" }
        };
        Dictionary<string, string?> second = new()
        {
            { "key1", null },
            { "key2", "value" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_OneNullOneEmptyString_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, string?> first = new()
        {
            { "key1", null }
        };
        Dictionary<string, string?> second = new()
        {
            { "key1", "" }
        };

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DictionaryEquals_LargeDictionaries_WorksCorrectly()
    {
        // Arrange
        Dictionary<string, int> first = new();
        Dictionary<string, int> second = new();

        for (int i = 0; i < 1000; i++)
        {
            first[$"key{i}"] = i;
            second[$"key{i}"] = i;
        }

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DictionaryEquals_LargeDictionariesOneDifferent_ReturnsFalse()
    {
        // Arrange
        Dictionary<string, int> first = new();
        Dictionary<string, int> second = new();

        for (int i = 0; i < 1000; i++)
        {
            first[$"key{i}"] = i;
            second[$"key{i}"] = i;
        }
        second["key500"] = 999; // Change one value

        // Act
        bool result = first.DictionaryEquals(second);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region Helper Classes

    /// <summary>
    /// Test object with custom Equals implementation
    /// </summary>
    private class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj is TestObject other)
            {
                return Id == other.Id && Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }

    #endregion
}
