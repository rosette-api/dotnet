using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for UnfieldedDateRecord class
/// </summary>
public class UnfieldedDateRecordTests
{
    #region Property Tests

    [Fact]
    public void Date_CanBeSetAndRetrieved()
    {
        // Arrange & Act
        var record = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Assert
        Assert.Equal("2024-03-10", record.Date);
    }

    [Fact]
    public void Date_CanBeUpdated()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "2024-01-01" };

        // Act
        record.Date = "2024-12-31";

        // Assert
        Assert.Equal("2024-12-31", record.Date);
    }

    [Fact]
    public void Date_AcceptsDifferentFormats()
    {
        // Arrange
        var formats = new[]
        {
            "2024-03-10",
            "03/10/2024",
            "March 10, 2024",
            "10-Mar-2024",
            "20240310"
        };

        // Act & Assert
        foreach (var format in formats)
        {
            var record = new UnfieldedDateRecord { Date = format };
            Assert.Equal(format, record.Date);
        }
    }

    [Fact]
    public void Date_HandlesPartialDates()
    {
        // Arrange & Act
        var record = new UnfieldedDateRecord { Date = "2024-03" };

        // Assert
        Assert.Equal("2024-03", record.Date);
    }

    [Fact]
    public void Date_HandlesRelativeDates()
    {
        // Arrange & Act
        var record = new UnfieldedDateRecord { Date = "yesterday" };

        // Assert
        Assert.Equal("yesterday", record.Date);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameDate_ReturnsTrue()
    {
        // Arrange
        var record1 = new UnfieldedDateRecord { Date = "2024-03-10" };
        var record2 = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentDate_ReturnsFalse()
    {
        // Arrange
        var record1 = new UnfieldedDateRecord { Date = "2024-03-10" };
        var record2 = new UnfieldedDateRecord { Date = "2024-03-11" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "2024-03-10" };
        var differentType = "2024-03-10";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameDate_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new UnfieldedDateRecord { Date = "2024-03-10" };
        var record2 = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentDate_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new UnfieldedDateRecord { Date = "2024-03-10" };
        var record2 = new UnfieldedDateRecord { Date = "2024-03-11" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_EmptyDate_ReturnsConsistentValue()
    {
        // Arrange
        var record1 = new UnfieldedDateRecord { Date = string.Empty };
        var record2 = new UnfieldedDateRecord { Date = string.Empty };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsDate()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = "2024-03-10" };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("2024-03-10", result);
    }

    [Fact]
    public void ToString_EmptyDate_ReturnsEmptyString()
    {
        // Arrange
        var record = new UnfieldedDateRecord { Date = string.Empty };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    #endregion
}
