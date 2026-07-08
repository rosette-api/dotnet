using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for simple record models: StringRecord, NumberRecord, BooleanRecord
/// </summary>
public class SimpleRecordModelsTests
{
    #region StringRecord Tests

    [Fact]
    public void StringRecord_WithText_CreatesRecord()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "test value" };

        // Assert
        Assert.Equal("test value", record.Text);
    }

    [Fact]
    public void StringRecord_Text_CanBeUpdated()
    {
        // Arrange
        var record = new StringRecord { Text = "initial" };

        // Act
        record.Text = "updated";

        // Assert
        Assert.Equal("updated", record.Text);
    }

    [Fact]
    public void StringRecord_HandlesSpecialCharacters()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Special: @#$%^&*()" };

        // Assert
        Assert.Equal("Special: @#$%^&*()", record.Text);
    }

    [Fact]
    public void StringRecord_HandlesUnicode()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Unicode: 你好世界 🌍" };

        // Assert
        Assert.Equal("Unicode: 你好世界 🌍", record.Text);
    }

    [Fact]
    public void StringRecord_ToString_ReturnsText()
    {
        // Arrange
        var record = new StringRecord { Text = "test" };

        // Act
        var result = record.ToString();

        // Assert
        Assert.Equal("test", result);
    }

    [Fact]
    public void StringRecord_Equals_SameText_ReturnsTrue()
    {
        // Arrange
        var record1 = new StringRecord { Text = "same" };
        var record2 = new StringRecord { Text = "same" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void StringRecord_Equals_DifferentText_ReturnsFalse()
    {
        // Arrange
        var record1 = new StringRecord { Text = "first" };
        var record2 = new StringRecord { Text = "second" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void StringRecord_GetHashCode_SameText_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new StringRecord { Text = "same" };
        var record2 = new StringRecord { Text = "same" };

        // Act & Assert
        Assert.Equal(record1.GetHashCode(), record2.GetHashCode());
    }

    #endregion

    #region NumberRecord Tests

    [Fact]
    public void NumberRecord_NoArgsConstructor_SetsNumberToZero()
    {
        // Arrange & Act
        var record = new NumberRecord();

        // Assert
        Assert.Equal(0, record.Number);
    }

    [Fact]
    public void NumberRecord_Constructor_SetsNumber()
    {
        // Arrange & Act
        var record = new NumberRecord(42.5);

        // Assert
        Assert.Equal(42.5, record.Number);
    }

    [Fact]
    public void NumberRecord_Constructor_SetsNegativeValue()
    {
        // Arrange & Act
        var record = new NumberRecord(-100.5);

        // Assert
        Assert.Equal(-100.5, record.Number);
    }

    [Fact]
    public void NumberRecord_Number_CanBeUpdated()
    {
        // Arrange
        var record = new NumberRecord(10);

        // Act
        record.Number = 20;

        // Assert
        Assert.Equal(20, record.Number);
    }

    [Fact]
    public void NumberRecord_ToString_ReturnsNumberAsString()
    {
        // Arrange
        var record = new NumberRecord(42.5);

        // Act
        var result = record.ToString();

        // Assert
        Assert.Equal("42.5", result);
    }

    [Fact]
    public void NumberRecord_Equals_SameNumber_ReturnsTrue()
    {
        // Arrange
        var record1 = new NumberRecord(100);
        var record2 = new NumberRecord(100);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void NumberRecord_Equals_DifferentNumber_ReturnsFalse()
    {
        // Arrange
        var record1 = new NumberRecord(100);
        var record2 = new NumberRecord(200);

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void NumberRecord_GetHashCode_SameNumber_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new NumberRecord(100);
        var record2 = new NumberRecord(100);

        // Act & Assert
        Assert.Equal(record1.GetHashCode(), record2.GetHashCode());
    }

    [Fact]
    public void NumberRecord_HandlesLargeNumbers()
    {
        // Arrange & Act
        var record = new NumberRecord(double.MaxValue);

        // Assert
        Assert.Equal(double.MaxValue, record.Number);
    }

    [Fact]
    public void NumberRecord_HandlesSmallNumbers()
    {
        // Arrange & Act
        var record = new NumberRecord(double.MinValue);

        // Assert
        Assert.Equal(double.MinValue, record.Number);
    }

    #endregion

    #region BooleanRecord Tests

    [Fact]
    public void BooleanRecord_NoArgsConstructor_SetsBooleanToFalse()
    {
        // Arrange & Act
        var record = new BooleanRecord();

        // Assert
        Assert.False(record.Boolean);
    }

    [Fact]
    public void BooleanRecord_Constructor_SetsTrue()
    {
        // Arrange & Act
        var record = new BooleanRecord(true);

        // Assert
        Assert.True(record.Boolean);
    }

    [Fact]
    public void BooleanRecord_Constructor_SetsFalse()
    {
        // Arrange & Act
        var record = new BooleanRecord(false);

        // Assert
        Assert.False(record.Boolean);
    }

    [Fact]
    public void BooleanRecord_Boolean_CanBeUpdated()
    {
        // Arrange
        var record = new BooleanRecord(false);

        // Act
        record.Boolean = true;

        // Assert
        Assert.True(record.Boolean);
    }

    [Fact]
    public void BooleanRecord_ToString_True_ReturnsLowercaseTrue()
    {
        // Arrange
        var record = new BooleanRecord(true);

        // Act
        var result = record.ToString();

        // Assert
        Assert.Equal("true", result);
    }

    [Fact]
    public void BooleanRecord_ToString_False_ReturnsLowercaseFalse()
    {
        // Arrange
        var record = new BooleanRecord(false);

        // Act
        var result = record.ToString();

        // Assert
        Assert.Equal("false", result);
    }

    [Fact]
    public void BooleanRecord_Equals_BothTrue_ReturnsTrue()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(true);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void BooleanRecord_Equals_BothFalse_ReturnsTrue()
    {
        // Arrange
        var record1 = new BooleanRecord(false);
        var record2 = new BooleanRecord(false);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void BooleanRecord_Equals_TrueAndFalse_ReturnsFalse()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(false);

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void BooleanRecord_GetHashCode_SameValue_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(true);

        // Act & Assert
        Assert.Equal(record1.GetHashCode(), record2.GetHashCode());
    }

    #endregion
}
