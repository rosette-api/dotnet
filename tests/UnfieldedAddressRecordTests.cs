using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

/// <summary>
/// Tests for UnfieldedAddressRecord class
/// </summary>
public class UnfieldedAddressRecordTests
{
    #region Property Tests

    [Fact]
    public void Address_CanBeSetAndRetrieved()
    {
        // Arrange & Act
        var record = new UnfieldedAddressRecord { Address = "123 Main Street, Springfield, IL 62701" };

        // Assert
        Assert.Equal("123 Main Street, Springfield, IL 62701", record.Address);
    }

    [Fact]
    public void Address_CanBeUpdated()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "Initial Address" };

        // Act
        record.Address = "456 Oak Avenue, Boston, MA 02101";

        // Assert
        Assert.Equal("456 Oak Avenue, Boston, MA 02101", record.Address);
    }

    [Fact]
    public void Address_HandlesComplexAddress()
    {
        // Arrange & Act
        var record = new UnfieldedAddressRecord { Address = "Apt 3B, 789 Pine St, Suite 200, New York, NY 10001" };

        // Assert
        Assert.Equal("Apt 3B, 789 Pine St, Suite 200, New York, NY 10001", record.Address);
    }

    [Fact]
    public void Address_HandlesInternationalAddress()
    {
        // Arrange & Act
        var record = new UnfieldedAddressRecord { Address = "10 Downing Street, London, SW1A 2AA, United Kingdom" };

        // Assert
        Assert.Equal("10 Downing Street, London, SW1A 2AA, United Kingdom", record.Address);
    }

    [Fact]
    public void Address_HandlesNonLatinCharacters()
    {
        // Arrange & Act
        var record = new UnfieldedAddressRecord { Address = "東京都渋谷区神南1丁目" };

        // Assert
        Assert.Equal("東京都渋谷区神南1丁目", record.Address);
    }

    [Fact]
    public void Address_HandlesSpecialCharacters()
    {
        // Arrange & Act
        var record = new UnfieldedAddressRecord { Address = "123 O'Brien St. #456" };

        // Assert
        Assert.Equal("123 O'Brien St. #456", record.Address);
    }

    [Fact]
    public void Address_HandlesMultiLineAddress()
    {
        // Arrange & Act
        var record = new UnfieldedAddressRecord { Address = "Building A\nFloor 5\n100 Main St\nCity, State 12345" };

        // Assert
        Assert.Equal("Building A\nFloor 5\n100 Main St\nCity, State 12345", record.Address);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameAddress_ReturnsTrue()
    {
        // Arrange
        var record1 = new UnfieldedAddressRecord { Address = "123 Main St" };
        var record2 = new UnfieldedAddressRecord { Address = "123 Main St" };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentAddress_ReturnsFalse()
    {
        // Arrange
        var record1 = new UnfieldedAddressRecord { Address = "123 Main St" };
        var record2 = new UnfieldedAddressRecord { Address = "456 Oak Ave" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "123 Main St" };

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "123 Main St" };
        var differentType = "123 Main St";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "123 Main St" };

        // Act & Assert
        Assert.True(record.Equals(record));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameAddress_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new UnfieldedAddressRecord { Address = "123 Main St" };
        var record2 = new UnfieldedAddressRecord { Address = "123 Main St" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentAddress_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new UnfieldedAddressRecord { Address = "123 Main St" };
        var record2 = new UnfieldedAddressRecord { Address = "456 Oak Ave" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_EmptyAddress_ReturnsConsistentValue()
    {
        // Arrange
        var record1 = new UnfieldedAddressRecord { Address = string.Empty };
        var record2 = new UnfieldedAddressRecord { Address = string.Empty };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsAddress()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = "123 Main St" };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal("123 Main St", result);
    }

    [Fact]
    public void ToString_EmptyAddress_ReturnsEmptyString()
    {
        // Arrange
        var record = new UnfieldedAddressRecord { Address = string.Empty };

        // Act
        string result = record.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    #endregion
}
