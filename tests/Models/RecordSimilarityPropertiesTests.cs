using Rosette.Api.Client.Models;
using System.Text.Json;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for RecordSimilarityProperties class
/// </summary>
public class RecordSimilarityPropertiesTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_SetsDefaultValues()
    {
        // Act
        var props = new RecordSimilarityProperties();

        // Assert
        Assert.Equal(0.0, props.Threshold);
        Assert.Null(props.IncludeExplainInfo);
        Assert.Null(props.Parameters);
        Assert.Null(props.ParameterUniverse);
    }

    [Fact]
    public void Constructor_WithIncludeExplainInfo_SetsCorrectValues()
    {
        // Act
        var props = new RecordSimilarityProperties(true);

        // Assert
        Assert.True(props.IncludeExplainInfo);
        Assert.Equal(0.0, props.Threshold);
        Assert.NotNull(props.Parameters);
        Assert.Empty(props.Parameters);
        Assert.Equal(string.Empty, props.ParameterUniverse);
    }

    [Fact]
    public void Constructor_WithIncludeExplainInfoFalse_SetsCorrectValues()
    {
        // Act
        var props = new RecordSimilarityProperties(false);

        // Assert
        Assert.False(props.IncludeExplainInfo);
        Assert.Equal(0.0, props.Threshold);
        Assert.NotNull(props.Parameters);
        Assert.Empty(props.Parameters);
        Assert.Equal(string.Empty, props.ParameterUniverse);
    }

    [Fact]
    public void Constructor_WithAllParameters_SetsAllValues()
    {
        // Arrange
        var parameters = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };

        // Act
        var props = new RecordSimilarityProperties(0.75, true, parameters, "test-universe");

        // Assert
        Assert.Equal(0.75, props.Threshold);
        Assert.True(props.IncludeExplainInfo);
        Assert.Equal(parameters, props.Parameters);
        Assert.Equal("test-universe", props.ParameterUniverse);
    }

    [Fact]
    public void Constructor_WithNullParameters_AcceptsNull()
    {
        // Act
        var props = new RecordSimilarityProperties(0.5, false, null, null);

        // Assert
        Assert.Equal(0.5, props.Threshold);
        Assert.False(props.IncludeExplainInfo);
        Assert.Null(props.Parameters);
        Assert.Null(props.ParameterUniverse);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Threshold_CanBeSet_ToValidValue()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.Threshold = 0.85;

        // Assert
        Assert.Equal(0.85, props.Threshold);
    }

    [Fact]
    public void Threshold_AcceptsZero()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.Threshold = 0.0;

        // Assert
        Assert.Equal(0.0, props.Threshold);
    }

    [Fact]
    public void Threshold_AcceptsOne()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.Threshold = 1.0;

        // Assert
        Assert.Equal(1.0, props.Threshold);
    }

    [Fact]
    public void Threshold_CanBeNull()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.Threshold = null;

        // Assert
        Assert.Null(props.Threshold);
    }

    [Fact]
    public void IncludeExplainInfo_CanBeToggled()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act & Assert
        props.IncludeExplainInfo = true;
        Assert.True(props.IncludeExplainInfo);

        props.IncludeExplainInfo = false;
        Assert.False(props.IncludeExplainInfo);

        props.IncludeExplainInfo = null;
        Assert.Null(props.IncludeExplainInfo);
    }

    [Fact]
    public void Parameters_CanBeSetAndModified()
    {
        // Arrange
        var props = new RecordSimilarityProperties();
        var parameters = new Dictionary<string, string>
        {
            { "param1", "value1" }
        };

        // Act
        props.Parameters = parameters;
        props.Parameters["param2"] = "value2";

        // Assert
        Assert.Equal(2, props.Parameters.Count);
        Assert.Equal("value1", props.Parameters["param1"]);
        Assert.Equal("value2", props.Parameters["param2"]);
    }

    [Fact]
    public void Parameters_CanBeEmpty()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.Parameters = new Dictionary<string, string>();

        // Assert
        Assert.NotNull(props.Parameters);
        Assert.Empty(props.Parameters);
    }

    [Fact]
    public void ParameterUniverse_CanBeSet()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.ParameterUniverse = "production-universe";

        // Assert
        Assert.Equal("production-universe", props.ParameterUniverse);
    }

    [Fact]
    public void ParameterUniverse_CanBeEmptyString()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        props.ParameterUniverse = string.Empty;

        // Assert
        Assert.Equal(string.Empty, props.ParameterUniverse);
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string> { { "key", "value" } }, "universe");
        var props2 = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string> { { "key", "value" } }, "universe");

        // Act & Assert
        Assert.True(props1.Equals(props2));
    }

    [Fact]
    public void Equals_DifferentThreshold_ReturnsFalse()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, null, null);
        var props2 = new RecordSimilarityProperties(0.80, true, null, null);

        // Act & Assert
        Assert.False(props1.Equals(props2));
    }

    [Fact]
    public void Equals_DifferentIncludeExplainInfo_ReturnsFalse()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, null, null);
        var props2 = new RecordSimilarityProperties(0.75, false, null, null);

        // Act & Assert
        Assert.False(props1.Equals(props2));
    }

    [Fact]
    public void Equals_DifferentParameters_ReturnsFalse()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string> { { "key", "value1" } }, null);
        var props2 = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string> { { "key", "value2" } }, null);

        // Act & Assert
        Assert.False(props1.Equals(props2));
    }

    [Fact]
    public void Equals_DifferentParameterUniverse_ReturnsFalse()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, null, "universe1");
        var props2 = new RecordSimilarityProperties(0.75, true, null, "universe2");

        // Act & Assert
        Assert.False(props1.Equals(props2));
    }

    [Fact]
    public void Equals_BothParametersNull_ReturnsTrue()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, null, "universe");
        var props2 = new RecordSimilarityProperties(0.75, true, null, "universe");

        // Act & Assert
        Assert.True(props1.Equals(props2));
    }

    [Fact]
    public void Equals_OneParameterNullOneNot_ReturnsFalse()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, null, null);
        var props2 = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string>(), null);

        // Act & Assert
        Assert.False(props1.Equals(props2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act & Assert
        Assert.False(props.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var props = new RecordSimilarityProperties();
        var differentType = "string";

        // Act & Assert
        Assert.False(props.Equals(differentType));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        // Arrange
        var props = new RecordSimilarityProperties(0.75, true, null, null);

        // Act & Assert
        Assert.True(props.Equals(props));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange - Use same dictionary reference for consistent hash
        var parameters = new Dictionary<string, string> { { "key", "value" } };
        var props1 = new RecordSimilarityProperties(0.75, true, parameters, "universe");
        var props2 = new RecordSimilarityProperties(0.75, true, parameters, "universe");

        // Act
        int hash1 = props1.GetHashCode();
        int hash2 = props2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange
        var props1 = new RecordSimilarityProperties(0.75, true, null, null);
        var props2 = new RecordSimilarityProperties(0.80, false, null, null);

        // Act
        int hash1 = props1.GetHashCode();
        int hash2 = props2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_NullParameters_ReturnsConsistentValue()
    {
        // Arrange
        var props = new RecordSimilarityProperties(0.75, true, null, null);

        // Act
        int hash1 = props.GetHashCode();
        int hash2 = props.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    #endregion

    #region Serialization Tests

    [Fact]
    public void Serialization_WithAllProperties_ProducesValidJson()
    {
        // Arrange
        var props = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string> { { "key", "value" } }, "test-universe");

        // Act
        string json = JsonSerializer.Serialize(props);

        // Assert
        Assert.Contains("\"threshold\"", json);
        Assert.Contains("0.75", json);
        Assert.Contains("\"includeExplainInfo\"", json);
        Assert.Contains("true", json);
        Assert.Contains("\"parameters\"", json);
        Assert.Contains("\"parameterUniverse\"", json);
        Assert.Contains("test-universe", json);
    }

    [Fact]
    public void Serialization_WithDefaults_ProducesValidJson()
    {
        // Arrange
        var props = new RecordSimilarityProperties();

        // Act
        string json = JsonSerializer.Serialize(props);

        // Assert
        Assert.Contains("\"threshold\"", json);
    }

    [Fact]
    public void Deserialization_FromJson_CreatesCorrectObject()
    {
        // Arrange
        string json = @"{
            ""threshold"": 0.85,
            ""includeExplainInfo"": true,
            ""parameters"": {""key1"": ""value1""},
            ""parameterUniverse"": ""test""
        }";

        // Act
        var props = JsonSerializer.Deserialize<RecordSimilarityProperties>(json);

        // Assert
        Assert.NotNull(props);
        Assert.Equal(0.85, props.Threshold);
        Assert.True(props.IncludeExplainInfo);
        Assert.Single(props.Parameters);
        Assert.Equal("value1", props.Parameters["key1"]);
        Assert.Equal("test", props.ParameterUniverse);
    }

    [Fact]
    public void RoundTrip_PreservesAllData()
    {
        // Arrange
        var original = new RecordSimilarityProperties(0.75, true, 
            new Dictionary<string, string> { { "key", "value" } }, "universe");

        // Act
        string json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<RecordSimilarityProperties>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Threshold, deserialized.Threshold);
        Assert.Equal(original.IncludeExplainInfo, deserialized.IncludeExplainInfo);
        Assert.Equal(original.ParameterUniverse, deserialized.ParameterUniverse);
    }

    #endregion
}
