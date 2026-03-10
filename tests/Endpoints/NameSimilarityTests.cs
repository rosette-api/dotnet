using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests.Endpoints;

public class NameSimilarityTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenNamesAreNull() {
        var exception = Record.Exception(() => new NameSimilarity(null, null));
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal("Value cannot be null. (Parameter 'name1')", exception.Message);

        Name rn = new("foo");
        exception = Record.Exception(() => new NameSimilarity(rn, null));
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal("Value cannot be null. (Parameter 'name2')", exception.Message);
    }

    [Fact]
    public void Constructor_SetsEndpointCorrectly_WhenCalledWithValidNames()
    {
        // Arrange
        var name1 = new Name("John Smith");
        var name2 = new Name("Jon Smyth");

        // Act
        var endpoint = new NameSimilarity(name1, name2);

        // Assert
        Assert.Equal("name-similarity", endpoint.Endpoint);
    }

    [Fact]
    public void Constructor_SetsParamsCorrectly_WhenBothNamesProvided()
    {
        // Arrange
        var name1 = new Name("Alice Johnson");
        var name2 = new Name("Alicia Johnston");

        // Act
        var endpoint = new NameSimilarity(name1, name2);

        // Assert
        Assert.Contains("name1", endpoint.Params.Keys);
        Assert.Contains("name2", endpoint.Params.Keys);
        Assert.Equal(name1, endpoint.Params["name1"]);
        Assert.Equal(name2, endpoint.Params["name2"]);
    }

    [Fact]
    public void Constructor_HandlesNamesWithLanguage_WhenProvided()
    {
        // Arrange
        var name1 = new Name("José García", language: "spa");
        var name2 = new Name("Jose Garcia", language: "eng");

        // Act
        var endpoint = new NameSimilarity(name1, name2);

        // Assert
        Assert.Equal(name1, endpoint.Params["name1"]);
        Assert.Equal(name2, endpoint.Params["name2"]);
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenName1IsNull()
    {
        // Arrange
        var name2 = new Name("Test Name");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new NameSimilarity(null, name2));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenName2IsNull()
    {
        // Arrange
        var name1 = new Name("Test Name");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new NameSimilarity(name1, null));
    }

    #endregion
}
