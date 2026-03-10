using Rosette.Api.Client.Models;
using System.Text.Json;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for FieldedDateRecord class
/// </summary>
public class FieldedDateRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_CreatesRecordWithEmptyDate()
    {
        // Act
        var record = new FieldedDateRecord { Date = string.Empty };

        // Assert
        Assert.Equal(string.Empty, record.Date);
        Assert.Null(record.Format);
    }

    [Fact]
    public void Constructor_WithDateOnly_SetsDateProperty()
    {
        // Arrange
        const string date = "2024-03-10";

        // Act
        var record = new FieldedDateRecord { Date = date };

        // Assert
        Assert.Equal(date, record.Date);
        Assert.Null(record.Format);
    }

    [Fact]
    public void Constructor_WithDateAndFormat_SetsBothProperties()
    {
        // Arrange
        const string date = "03/10/2024";
        const string format = "MM/dd/yyyy";

        // Act
        var record = new FieldedDateRecord
        {
            Date = date,
            Format = format
        };

        // Assert
        Assert.Equal(date, record.Date);
        Assert.Equal(format, record.Format);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Date_CanBeSet_WithValidValue()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-01-01" };

        // Act
        record.Date = "2024-12-31";

        // Assert
        Assert.Equal("2024-12-31", record.Date);
    }

    [Fact]
    public void Format_CanBeSet_WithValidValue()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-03-10" };

        // Act
        record.Format = "yyyy-MM-dd";

        // Assert
        Assert.Equal("yyyy-MM-dd", record.Format);
    }

    [Fact]
    public void Format_AcceptsJavaDateTimeFormatterPatterns()
    {
        // Arrange
        var formats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd.MM.yyyy",
            "yyyy/MM/dd",
            "yyyyMMdd",
            "dd-MMM-yyyy"
        };

        // Act & Assert
        foreach (var format in formats)
        {
            var record = new FieldedDateRecord { Date = "2024-03-10", Format = format };
            Assert.Equal(format, record.Format);
        }
    }

    [Fact]
    public void Date_AcceptsDifferentDateFormats()
    {
        // Arrange
        var dates = new[]
        {
            "2024-03-10",
            "03/10/2024",
            "March 10, 2024",
            "10-Mar-2024",
            "20240310"
        };

        // Act & Assert
        foreach (var date in dates)
        {
            var record = new FieldedDateRecord { Date = date };
            Assert.Equal(date, record.Date);
        }
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameDateAndFormat_ReturnsTrue()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };
        var record2 = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_SameDateNoFormat_ReturnsTrue()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10" };
        var record2 = new FieldedDateRecord { Date = "2024-03-10" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentDate_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10" };
        var record2 = new FieldedDateRecord { Date = "2024-03-11" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentFormat_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };
        var record2 = new FieldedDateRecord { Date = "2024-03-10", Format = "MM/dd/yyyy" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_OneWithFormatOneWithout_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };
        var record2 = new FieldedDateRecord { Date = "2024-03-10" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-03-10" };

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-03-10" };
        var differentType = "2024-03-10";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };
        var record2 = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentProperties_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new FieldedDateRecord { Date = "2024-03-10" };
        var record2 = new FieldedDateRecord { Date = "2024-03-11" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsValidJson()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };

        // Act
        string json = record.ToString();

        // Assert
        Assert.NotEmpty(json);
        Assert.Contains("\"date\"", json);
        Assert.Contains("2024-03-10", json);
    }

    [Fact]
    public void ToString_CanBeDeserialized()
    {
        // Arrange
        var original = new FieldedDateRecord { Date = "2024-03-10", Format = "yyyy-MM-dd" };

        // Act
        string json = original.ToString();
        var deserialized = JsonSerializer.Deserialize<FieldedDateRecord>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Date, deserialized.Date);
        Assert.Equal(original.Format, deserialized.Format);
    }

    [Fact]
    public void ToString_NullFormat_ProducesValidJson()
    {
        // Arrange
        var record = new FieldedDateRecord { Date = "2024-03-10" };

        // Act
        string json = record.ToString();
        var deserialized = JsonSerializer.Deserialize<FieldedDateRecord>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal("2024-03-10", deserialized.Date);
    }

    #endregion
}
