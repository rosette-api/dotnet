using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for UnfieldedNameRecord class
/// </summary>
public class UnfieldedNameRecordTests
{
    #region Property Tests

    [Fact]
    public void Text_CanBeSetAndRetrieved()
    {
        // Arrange & Act
        var record = new UnfieldedNameRecord { Text = "John Smith" };

        // Assert
        Assert.Equal("John Smith", record.Text);
    }

    [Fact]
    public void Text_CanBeUpdated()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "Initial" };

        // Act
        record.Text = "Updated";

        // Assert
        Assert.Equal("Updated", record.Text);
    }

    [Fact]
    public void Text_HandlesSpecialCharacters()
    {
        // Arrange & Act
        var record = new UnfieldedNameRecord { Text = "O'Brien-Smith" };

        // Assert
        Assert.Equal("O'Brien-Smith", record.Text);
    }

    [Fact]
    public void Text_HandlesUnicodeCharacters()
    {
        // Arrange & Act
        var record = new UnfieldedNameRecord { Text = "José García" };

        // Assert
        Assert.Equal("José García", record.Text);
    }

    [Fact]
    public void Text_HandlesNonLatinScripts()
    {
        // Arrange & Act
        var record = new UnfieldedNameRecord { Text = "山田太郎" };

        // Assert
        Assert.Equal("山田太郎", record.Text);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameText_ReturnsTrue()
    {
        // Arrange
        var record1 = new UnfieldedNameRecord { Text = "John Smith" };
        var record2 = new UnfieldedNameRecord { Text = "John Smith" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentText_ReturnsFalse()
    {
        // Arrange
        var record1 = new UnfieldedNameRecord { Text = "John Smith" };
        var record2 = new UnfieldedNameRecord { Text = "Jane Doe" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "John Smith" };

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "John Smith" };
        var differentType = "John Smith";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = "John Smith" };

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameText_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new UnfieldedNameRecord { Text = "John Smith" };
        var record2 = new UnfieldedNameRecord { Text = "John Smith" };

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
        var record1 = new UnfieldedNameRecord { Text = "John Smith" };
        var record2 = new UnfieldedNameRecord { Text = "Jane Doe" };

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
        var record1 = new UnfieldedNameRecord { Text = string.Empty };
        var record2 = new UnfieldedNameRecord { Text = string.Empty };

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
        var record = new UnfieldedNameRecord { Text = "John Smith" };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("John Smith", result);
    }

    [Fact]
    public void ToString_EmptyText_ReturnsEmptyString()
    {
        // Arrange
        var record = new UnfieldedNameRecord { Text = string.Empty };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    #endregion
}
