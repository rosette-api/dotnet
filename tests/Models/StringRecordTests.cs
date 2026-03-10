using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for StringRecord class
/// </summary>
public class StringRecordTests
{
    #region Property Tests

    [Fact]
    public void Text_CanBeSetAndRetrieved()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Hello World" };

        // Assert
        Assert.Equal("Hello World", record.Text);
    }

    [Fact]
    public void Text_CanBeUpdated()
    {
        // Arrange
        var record = new StringRecord { Text = "Initial" };

        // Act
        record.Text = "New Value";

        // Assert
        Assert.Equal("New Value", record.Text);
    }

    [Fact]
    public void Text_HandlesLongStrings()
    {
        // Arrange
        var longString = new string('A', 10000);

        // Act
        var record = new StringRecord { Text = longString };

        // Assert
        Assert.Equal(longString, record.Text);
        Assert.Equal(10000, record.Text.Length);
    }

    [Fact]
    public void Text_HandlesSpecialCharacters()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Special: !@#$%^&*()_+-={}[]|\\:;\"'<>,.?/~`" };

        // Assert
        Assert.Equal("Special: !@#$%^&*()_+-={}[]|\\:;\"'<>,.?/~`", record.Text);
    }

    [Fact]
    public void Text_HandlesUnicodeCharacters()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Unicode: 你好世界 مرحبا العالم" };

        // Assert
        Assert.Equal("Unicode: 你好世界 مرحبا العالم", record.Text);
    }

    [Fact]
    public void Text_HandlesEmojis()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Emojis: 😀🎉🌟❤️" };

        // Assert
        Assert.Equal("Emojis: 😀🎉🌟❤️", record.Text);
    }

    [Fact]
    public void Text_HandlesMultilineText()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Line 1\nLine 2\nLine 3" };

        // Assert
        Assert.Equal("Line 1\nLine 2\nLine 3", record.Text);
    }

    [Fact]
    public void Text_HandlesTabs()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Column1\tColumn2\tColumn3" };

        // Assert
        Assert.Equal("Column1\tColumn2\tColumn3", record.Text);
    }

    [Fact]
    public void Text_HandlesControlCharacters()
    {
        // Arrange & Act
        var record = new StringRecord { Text = "Text\r\nWith\r\nControl\r\nCharacters" };

        // Assert
        Assert.Equal("Text\r\nWith\r\nControl\r\nCharacters", record.Text);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameText_ReturnsTrue()
    {
        // Arrange
        var record1 = new StringRecord { Text = "Test" };
        var record2 = new StringRecord { Text = "Test" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentText_ReturnsFalse()
    {
        // Arrange
        var record1 = new StringRecord { Text = "Test1" };
        var record2 = new StringRecord { Text = "Test2" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_CaseSensitive_ReturnsFalse()
    {
        // Arrange
        var record1 = new StringRecord { Text = "Test" };
        var record2 = new StringRecord { Text = "test" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new StringRecord { Text = "Test" };

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new StringRecord { Text = "Test" };
        var differentType = "Test";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new StringRecord { Text = "Test" };

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    [Fact]
    public void Equals_WhitespaceMatters()
    {
        // Arrange
        var record1 = new StringRecord { Text = "Test " };
        var record2 = new StringRecord { Text = "Test" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameText_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new StringRecord { Text = "Test" };
        var record2 = new StringRecord { Text = "Test" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentText_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new StringRecord { Text = "Test1" };
        var record2 = new StringRecord { Text = "Test2" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_EmptyText_ReturnsConsistentValue()
    {
        // Arrange
        var record1 = new StringRecord { Text = string.Empty };
        var record2 = new StringRecord { Text = string.Empty };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsText()
    {
        // Arrange
        var record = new StringRecord { Text = "Hello World" };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("Hello World", result);
    }

    [Fact]
    public void ToString_EmptyText_ReturnsEmptyString()
    {
        // Arrange
        var record = new StringRecord { Text = string.Empty };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToString_PreservesFormatting()
    {
        // Arrange
        var record = new StringRecord { Text = "Line 1\n\tLine 2\nLine 3" };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("Line 1\n\tLine 2\nLine 3", result);
    }

    #endregion
}
