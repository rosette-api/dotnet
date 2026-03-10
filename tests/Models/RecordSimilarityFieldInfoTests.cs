using Rosette.Api.Client.Models;
using System.Text.Json;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for RecordSimilarityFieldInfo class
/// </summary>
public class RecordSimilarityFieldInfoTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_SetsDefaultValues()
    {
        // Act
        var fieldInfo = new RecordSimilarityFieldInfo();

        // Assert
        Assert.Null(fieldInfo.Type);
        Assert.Null(fieldInfo.Weight);
        Assert.Null(fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void Constructor_WithAllParameters_SetsAllValues()
    {
        // Act
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Assert
        Assert.Equal("rni_name", fieldInfo.Type);
        Assert.Equal(0.8, fieldInfo.Weight);
        Assert.Equal(0.5, fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void Constructor_WithNullValues_AcceptsNulls()
    {
        // Act
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", null, null);

        // Assert
        Assert.Equal("rni_name", fieldInfo.Type);
        Assert.Null(fieldInfo.Weight);
        Assert.Null(fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void Constructor_WithZeroWeight_AcceptsZero()
    {
        // Act
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.0, 0.0);

        // Assert
        Assert.Equal(0.0, fieldInfo.Weight);
        Assert.Equal(0.0, fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void Constructor_WithMaxWeight_AcceptsOne()
    {
        // Act
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 1.0, 1.0);

        // Assert
        Assert.Equal(1.0, fieldInfo.Weight);
        Assert.Equal(1.0, fieldInfo.ScoreIfNull);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Type_CanBeSet_ToValidAlgorithm()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo();
        var validTypes = new[] { "rni_name", "rni_date", "rni_address", "string" };

        // Act & Assert
        foreach (var type in validTypes)
        {
            fieldInfo.Type = type;
            Assert.Equal(type, fieldInfo.Type);
        }
    }

    [Fact]
    public void Type_CanBeSetToEmptyString()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo();

        // Act
        fieldInfo.Type = string.Empty;

        // Assert
        Assert.Equal(string.Empty, fieldInfo.Type);
    }

    [Fact]
    public void Weight_CanBeSet_ToValidRange()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo();

        // Act & Assert
        fieldInfo.Weight = 0.0;
        Assert.Equal(0.0, fieldInfo.Weight);

        fieldInfo.Weight = 0.5;
        Assert.Equal(0.5, fieldInfo.Weight);

        fieldInfo.Weight = 1.0;
        Assert.Equal(1.0, fieldInfo.Weight);
    }

    [Fact]
    public void Weight_CanBeSetToNull()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("type", 0.8, null);

        // Act
        fieldInfo.Weight = null;

        // Assert
        Assert.Null(fieldInfo.Weight);
    }

    [Fact]
    public void ScoreIfNull_CanBeSet_ToValidRange()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo();

        // Act & Assert
        fieldInfo.ScoreIfNull = 0.0;
        Assert.Equal(0.0, fieldInfo.ScoreIfNull);

        fieldInfo.ScoreIfNull = 0.5;
        Assert.Equal(0.5, fieldInfo.ScoreIfNull);

        fieldInfo.ScoreIfNull = 1.0;
        Assert.Equal(1.0, fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void ScoreIfNull_CanBeSetToNull()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("type", null, 0.5);

        // Act
        fieldInfo.ScoreIfNull = null;

        // Assert
        Assert.Null(fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void Properties_CanBeModifiedIndependently()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo();

        // Act
        fieldInfo.Type = "rni_name";
        Assert.Equal("rni_name", fieldInfo.Type);
        Assert.Null(fieldInfo.Weight);

        fieldInfo.Weight = 0.8;
        Assert.Equal(0.8, fieldInfo.Weight);
        Assert.Null(fieldInfo.ScoreIfNull);

        fieldInfo.ScoreIfNull = 0.5;
        Assert.Equal(0.5, fieldInfo.ScoreIfNull);

        // Assert all are set
        Assert.Equal("rni_name", fieldInfo.Type);
        Assert.Equal(0.8, fieldInfo.Weight);
        Assert.Equal(0.5, fieldInfo.ScoreIfNull);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act & Assert
        Assert.True(fieldInfo1.Equals(fieldInfo2));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_date", 0.8, 0.5);

        // Act & Assert
        Assert.False(fieldInfo1.Equals(fieldInfo2));
    }

    [Fact]
    public void Equals_DifferentWeight_ReturnsFalse()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_name", 0.7, 0.5);

        // Act & Assert
        Assert.False(fieldInfo1.Equals(fieldInfo2));
    }

    [Fact]
    public void Equals_DifferentScoreIfNull_ReturnsFalse()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.4);

        // Act & Assert
        Assert.False(fieldInfo1.Equals(fieldInfo2));
    }

    [Fact]
    public void Equals_BothNullWeights_ReturnsTrue()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", null, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_name", null, 0.5);

        // Act & Assert
        Assert.True(fieldInfo1.Equals(fieldInfo2));
    }

    [Fact]
    public void Equals_OneNullWeightOneNot_ReturnsFalse()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", null, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act & Assert
        Assert.False(fieldInfo1.Equals(fieldInfo2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act & Assert
        Assert.False(fieldInfo.Equals(null));
    }

    [Fact]
    public void Equals_DifferentObjectType_ReturnsFalse()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var differentType = "string";

        // Act & Assert
        Assert.False(fieldInfo.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act & Assert
        Assert.True(fieldInfo.Equals(fieldInfo));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act
        int hash1 = fieldInfo1.GetHashCode();
        int hash2 = fieldInfo2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange
        var fieldInfo1 = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);
        var fieldInfo2 = new RecordSimilarityFieldInfo("rni_date", 0.7, 0.4);

        // Act
        int hash1 = fieldInfo1.GetHashCode();
        int hash2 = fieldInfo2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_NullValues_ReturnsConsistentValue()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("type", null, null);

        // Act
        int hash1 = fieldInfo.GetHashCode();
        int hash2 = fieldInfo.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsValidJson()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act
        string json = fieldInfo.ToString();

        // Assert
        Assert.Contains("\"type\"", json);
        Assert.Contains("rni_name", json);
        Assert.Contains("\"weight\"", json);
        Assert.Contains("0.8", json);
        Assert.Contains("\"scoreIfNull\"", json);
        Assert.Contains("0.5", json);
    }

    [Fact]
    public void ToString_WithNullValues_ProducesValidJson()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", null, null);

        // Act
        string json = fieldInfo.ToString();

        // Assert
        Assert.Contains("\"type\"", json);
        Assert.Contains("rni_name", json);
    }

    [Fact]
    public void ToString_CanBeParsedBack()
    {
        // Arrange
        var original = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act
        string json = original.ToString();
        var parsed = JsonSerializer.Deserialize<RecordSimilarityFieldInfo>(json);

        // Assert
        Assert.NotNull(parsed);
        Assert.Equal(original.Type, parsed.Type);
        Assert.Equal(original.Weight, parsed.Weight);
        Assert.Equal(original.ScoreIfNull, parsed.ScoreIfNull);
    }

    #endregion

    #region Serialization Tests

    [Fact]
    public void Serialization_WithAllProperties_ProducesValidJson()
    {
        // Arrange
        var fieldInfo = new RecordSimilarityFieldInfo("rni_name", 0.8, 0.5);

        // Act
        string json = JsonSerializer.Serialize(fieldInfo);

        // Assert
        Assert.Contains("\"type\"", json);
        Assert.Contains("\"weight\"", json);
        Assert.Contains("\"scoreIfNull\"", json);
    }

    [Fact]
    public void Deserialization_FromJson_CreatesCorrectObject()
    {
        // Arrange
        string json = @"{
            ""type"": ""rni_date"",
            ""weight"": 0.9,
            ""scoreIfNull"": 0.3
        }";

        // Act
        var fieldInfo = JsonSerializer.Deserialize<RecordSimilarityFieldInfo>(json);

        // Assert
        Assert.NotNull(fieldInfo);
        Assert.Equal("rni_date", fieldInfo.Type);
        Assert.Equal(0.9, fieldInfo.Weight);
        Assert.Equal(0.3, fieldInfo.ScoreIfNull);
    }

    [Fact]
    public void RoundTrip_PreservesAllData()
    {
        // Arrange
        var original = new RecordSimilarityFieldInfo("rni_address", 0.75, 0.25);

        // Act
        string json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<RecordSimilarityFieldInfo>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Type, deserialized.Type);
        Assert.Equal(original.Weight, deserialized.Weight);
        Assert.Equal(original.ScoreIfNull, deserialized.ScoreIfNull);
    }

    #endregion

    #region Validation Tests

    [Fact]
    public void Type_SupportsCommonAlgorithms()
    {
        // Arrange
        var algorithms = new[] 
        { 
            "rni_name", 
            "rni_date", 
            "rni_address",
            "string",
            "number",
            "boolean"
        };

        // Act & Assert
        foreach (var algorithm in algorithms)
        {
            var fieldInfo = new RecordSimilarityFieldInfo(algorithm, 0.8, 0.5);
            Assert.Equal(algorithm, fieldInfo.Type);
        }
    }

    [Fact]
    public void Weight_AcceptsBoundaryValues()
    {
        // Act & Assert
        var fieldInfo1 = new RecordSimilarityFieldInfo("type", 0.0, null);
        Assert.Equal(0.0, fieldInfo1.Weight);

        var fieldInfo2 = new RecordSimilarityFieldInfo("type", 1.0, null);
        Assert.Equal(1.0, fieldInfo2.Weight);
    }

    [Fact]
    public void ScoreIfNull_AcceptsBoundaryValues()
    {
        // Act & Assert
        var fieldInfo1 = new RecordSimilarityFieldInfo("type", null, 0.0);
        Assert.Equal(0.0, fieldInfo1.ScoreIfNull);

        var fieldInfo2 = new RecordSimilarityFieldInfo("type", null, 1.0);
        Assert.Equal(1.0, fieldInfo2.ScoreIfNull);
    }

    #endregion
}
