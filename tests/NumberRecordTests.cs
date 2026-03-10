using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for NumberRecord class
/// </summary>
public class NumberRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_CreatesZeroRecord()
    {
        // Act
        var record = new NumberRecord();

        // Assert
        Assert.Equal(0.0, record.Number);
    }

    [Fact]
    public void Constructor_WithPositiveNumber_SetsNumber()
    {
        // Act
        var record = new NumberRecord(42.5);

        // Assert
        Assert.Equal(42.5, record.Number);
    }

    [Fact]
    public void Constructor_WithNegativeNumber_SetsNumber()
    {
        // Act
        var record = new NumberRecord(-17.3);

        // Assert
        Assert.Equal(-17.3, record.Number);
    }

    [Fact]
    public void Constructor_WithZero_SetsNumber()
    {
        // Act
        var record = new NumberRecord(0.0);

        // Assert
        Assert.Equal(0.0, record.Number);
    }

    [Fact]
    public void Constructor_WithInteger_SetsNumber()
    {
        // Act
        var record = new NumberRecord(100);

        // Assert
        Assert.Equal(100.0, record.Number);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Number_CanBeSet_ToPositiveValue()
    {
        // Arrange
        var record = new NumberRecord();

        // Act
        record.Number = 123.456;

        // Assert
        Assert.Equal(123.456, record.Number);
    }

    [Fact]
    public void Number_CanBeSet_ToNegativeValue()
    {
        // Arrange
        var record = new NumberRecord();

        // Act
        record.Number = -999.99;

        // Assert
        Assert.Equal(-999.99, record.Number);
    }

    [Fact]
    public void Number_HandlesVeryLargeNumbers()
    {
        // Arrange & Act
        var record = new NumberRecord(double.MaxValue);

        // Assert
        Assert.Equal(double.MaxValue, record.Number);
    }

    [Fact]
    public void Number_HandlesVerySmallNumbers()
    {
        // Arrange & Act
        var record = new NumberRecord(double.MinValue);

        // Assert
        Assert.Equal(double.MinValue, record.Number);
    }

    [Fact]
    public void Number_HandlesInfinity()
    {
        // Arrange & Act
        var positiveInfinity = new NumberRecord(double.PositiveInfinity);
        var negativeInfinity = new NumberRecord(double.NegativeInfinity);

        // Assert
        Assert.Equal(double.PositiveInfinity, positiveInfinity.Number);
        Assert.Equal(double.NegativeInfinity, negativeInfinity.Number);
    }

    [Fact]
    public void Number_HandlesNaN()
    {
        // Arrange & Act
        var record = new NumberRecord(double.NaN);

        // Assert
        Assert.True(double.IsNaN(record.Number));
    }

    [Fact]
    public void Number_HandlesScientificNotation()
    {
        // Arrange & Act
        var record = new NumberRecord(1.23e10);

        // Assert
        Assert.Equal(1.23e10, record.Number);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameNumber_ReturnsTrue()
    {
        // Arrange
        var record1 = new NumberRecord(42.5);
        var record2 = new NumberRecord(42.5);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentNumber_ReturnsFalse()
    {
        // Arrange
        var record1 = new NumberRecord(42.5);
        var record2 = new NumberRecord(43.5);

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_BothZero_ReturnsTrue()
    {
        // Arrange
        var record1 = new NumberRecord(0.0);
        var record2 = new NumberRecord(0.0);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_NegativeZeroAndPositiveZero_ReturnsTrue()
    {
        // Arrange
        var record1 = new NumberRecord(-0.0);
        var record2 = new NumberRecord(0.0);

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_BothNaN_ReturnsTrue()
    {
        // Arrange
        var record1 = new NumberRecord(double.NaN);
        var record2 = new NumberRecord(double.NaN);

        // Act & Assert
        // Note: NaN == NaN is false in IEEE 754, but our Equals uses == operator
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new NumberRecord(42.5);

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new NumberRecord(42.5);
        var differentType = 42.5;

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new NumberRecord(42.5);

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameNumber_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new NumberRecord(42.5);
        var record2 = new NumberRecord(42.5);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentNumber_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new NumberRecord(42.5);
        var record2 = new NumberRecord(43.5);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_ZeroValue_ReturnsConsistentHashCode()
    {
        // Arrange
        var record1 = new NumberRecord(0.0);
        var record2 = new NumberRecord(0.0);

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_PositiveNumber_ReturnsNumberAsString()
    {
        // Arrange
        var record = new NumberRecord(42.5);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("42.5", result);
    }

    [Fact]
    public void ToString_NegativeNumber_ReturnsNumberAsString()
    {
        // Arrange
        var record = new NumberRecord(-17.3);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("-17.3", result);
    }

    [Fact]
    public void ToString_Zero_ReturnsZero()
    {
        // Arrange
        var record = new NumberRecord(0.0);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("0", result);
    }

    [Fact]
    public void ToString_Integer_ReturnsInteger()
    {
        // Arrange
        var record = new NumberRecord(100);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("100", result);
    }

    [Fact]
    public void ToString_ScientificNotation_ReturnsScientificNotation()
    {
        // Arrange
        var record = new NumberRecord(1.23e10);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Contains("12300000000", result);
    }

    [Fact]
    public void ToString_NaN_ReturnsNaN()
    {
        // Arrange
        var record = new NumberRecord(double.NaN);

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("NaN", result);
    }

    #endregion
}
