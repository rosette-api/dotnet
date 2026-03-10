using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for BooleanRecord class
/// </summary>
public class BooleanRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_CreatesFalseRecord()
    {
        // Act
        var record = new BooleanRecord();

        // Assert
        Assert.False(record.Boolean);
    }

    [Fact]
    public void Constructor_WithTrue_SetsBooleanToTrue()
    {
        // Act
        var record = new BooleanRecord(true);

        // Assert
        Assert.True(record.Boolean);
    }

    [Fact]
    public void Constructor_WithFalse_SetsBooleanToFalse()
    {
        // Act
        var record = new BooleanRecord(false);

        // Assert
        Assert.False(record.Boolean);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Boolean_CanBeSetToTrue()
    {
        // Arrange
        var record = new BooleanRecord();

        // Act
        record.Boolean = true;

        // Assert
        Assert.True(record.Boolean);
    }

    [Fact]
    public void Boolean_CanBeSetToFalse()
    {
        // Arrange
        var record = new BooleanRecord(true);

        // Act
        record.Boolean = false;

        // Assert
        Assert.False(record.Boolean);
    }

    [Fact]
    public void Boolean_CanBeToggledMultipleTimes()
    {
        // Arrange
        var record = new BooleanRecord();

        // Act & Assert
        record.Boolean = true;
        Assert.True(record.Boolean);

        record.Boolean = false;
        Assert.False(record.Boolean);

        record.Boolean = true;
        Assert.True(record.Boolean);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_BothTrue_ReturnsTrue()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(true);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_BothFalse_ReturnsTrue()
    {
        // Arrange
        var record1 = new BooleanRecord(false);
        var record2 = new BooleanRecord(false);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_OneTrueOneFalse_ReturnsFalse()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(false);

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new BooleanRecord(true);

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new BooleanRecord(true);
        var differentType = true;

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new BooleanRecord(true);

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_BothTrue_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(true);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_BothFalse_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new BooleanRecord(false);
        var record2 = new BooleanRecord(false);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_TrueAndFalse_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new BooleanRecord(true);
        var record2 = new BooleanRecord(false);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_True_ReturnsLowercaseTrue()
    {
        // Arrange
        var record = new BooleanRecord(true);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("true", result);
    }

    [Fact]
    public void ToString_False_ReturnsLowercaseFalse()
    {
        // Arrange
        var record = new BooleanRecord(false);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("false", result);
    }

    [Fact]
    public void ToString_ReturnsLowercaseFormat()
    {
        // Arrange
        var trueRecord = new BooleanRecord(true);
        var falseRecord = new BooleanRecord(false);

        // Act
        string trueResult = trueRecord.ToString();
        string falseResult = falseRecord.ToString();

        // Assert - Verify lowercase (not "True" or "False")
        Assert.Equal("true", trueResult);
        Assert.Equal("false", falseResult);
        Assert.NotEqual("True", trueResult);
        Assert.NotEqual("False", falseResult);
    }

    #endregion
}
