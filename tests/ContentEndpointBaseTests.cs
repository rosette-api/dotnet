using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Tests;

// Concrete test implementation of ContentEndpointBase for testing
internal class TestContentEndpoint : ContentEndpointBase<TestContentEndpoint>
{
    public TestContentEndpoint(object content) : base("test-endpoint", content)
    {
    }
}

public class ContentEndpointBaseTests
{
    [Fact]
    public void Constructor_ValidContent_SetsContentAndEndpoint()
    {
        // Arrange & Act
        var endpoint = new TestContentEndpoint("test content");

        // Assert
        Assert.Equal("test-endpoint", endpoint.Endpoint);
        Assert.Equal("test content", endpoint.Content);
    }

    [Fact]
    public void Constructor_NullContent_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestContentEndpoint(null!));
    }

    [Fact]
    public void Constructor_UriContent_SetsContentAsUriString()
    {
        // Arrange
        var uri = new Uri("http://example.com/document");

        // Act
        var endpoint = new TestContentEndpoint(uri);

        // Assert
        Assert.Equal("http://example.com/document", endpoint.Content.ToString());
    }

    [Fact]
    public void SetContent_ValidString_UpdatesContent()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("initial content");

        // Act
        var result = endpoint.SetContent("updated content");

        // Assert
        Assert.Equal("updated content", endpoint.Content);
        Assert.Same(endpoint, result); // Verify fluent API
    }

    [Fact]
    public void SetContent_NullContent_ThrowsArgumentNullException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("initial content");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => endpoint.SetContent(null!));
    }

    [Fact]
    public void SetContent_Uri_UpdatesContentToUriString()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("initial content");
        var uri = new Uri("http://example.com/new");

        // Act
        endpoint.SetContent(uri);

        // Assert
        Assert.Equal("http://example.com/new", endpoint.Content.ToString());
    }

    [Fact]
    public void Content_AfterConstruction_ReturnsProvidedContent()
    {
        // Arrange & Act
        var endpoint = new TestContentEndpoint("test content");

        // Assert
        Assert.Equal("test content", endpoint.Content);
    }

    [Fact]
    public void SetLanguage_ValidLanguageCode_SetsLanguage()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var result = endpoint.SetLanguage("eng");

        // Assert
        Assert.Equal("eng", endpoint.Language);
        Assert.Same(endpoint, result); // Verify fluent API
    }

    [Fact]
    public void SetLanguage_NullLanguage_ThrowsArgumentNullException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => endpoint.SetLanguage(null!));
    }

    [Fact]
    public void SetLanguage_EmptyLanguage_ThrowsArgumentException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => endpoint.SetLanguage(string.Empty));
    }

    [Fact]
    public void SetLanguage_WhitespaceLanguage_ThrowsArgumentException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => endpoint.SetLanguage("   "));
    }

    [Fact]
    public void Language_WithoutSettingLanguage_ReturnsEmptyString()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var language = endpoint.Language;

        // Assert
        Assert.Equal(string.Empty, language);
    }

    [Fact]
    public void Language_AfterSettingLanguage_ReturnsSetValue()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");
        endpoint.SetLanguage("spa");

        // Act
        var language = endpoint.Language;

        // Assert
        Assert.Equal("spa", language);
    }

    [Fact]
    public void SetGenre_AnyValue_IsIgnoredAndReturnsInstance()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var result = endpoint.SetGenre("social-media");

        // Assert
        Assert.Same(endpoint, result); // Verify fluent API
        Assert.Equal(string.Empty, endpoint.Genre); // Genre should remain empty
    }

    [Fact]
    public void Genre_WithoutSettingGenre_ReturnsEmptyString()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var genre = endpoint.Genre;

        // Assert
        Assert.Equal(string.Empty, genre);
    }

    [Fact]
    public void Genre_AfterSettingGenre_RemainsEmptyBecauseIgnored()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");
        endpoint.SetGenre("news");

        // Act
        var genre = endpoint.Genre;

        // Assert
        Assert.Equal(string.Empty, genre); // Genre is ignored in ContentEndpointBase
    }

    [Fact]
    public void SetFileContentType_ValidContentType_SetsFileContentType()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var result = endpoint.SetFileContentType("application/pdf");

        // Assert
        Assert.Equal("application/pdf", endpoint.FileContentType);
        Assert.Same(endpoint, result); // Verify fluent API
    }

    [Fact]
    public void SetFileContentType_NullContentType_ThrowsArgumentNullException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => endpoint.SetFileContentType(null!));
    }

    [Fact]
    public void SetFileContentType_EmptyContentType_ThrowsArgumentException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => endpoint.SetFileContentType(string.Empty));
    }

    [Fact]
    public void SetFileContentType_WhitespaceContentType_ThrowsArgumentException()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => endpoint.SetFileContentType("   "));
    }

    [Fact]
    public void FileContentType_Default_ReturnsTextPlain()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var contentType = endpoint.FileContentType;

        // Assert
        Assert.Equal("text/plain", contentType); // Default from EndpointExecutor
    }

    [Fact]
    public void FileContentType_AfterSetting_ReturnsSetValue()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");
        endpoint.SetFileContentType("application/json");

        // Act
        var contentType = endpoint.FileContentType;

        // Assert
        Assert.Equal("application/json", contentType);
    }

    [Fact]
    public void Filename_WithoutFileStream_ReturnsEmptyString()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        var filename = endpoint.Filename;

        // Assert
        Assert.Equal(string.Empty, filename);
    }

    [Fact]
    public void FluentAPI_ChainMultipleMethods_AllSettersReturnSameInstance()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("initial content");

        // Act
        var result = endpoint
            .SetContent("updated content")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetFileContentType("application/json");

        // Assert
        Assert.Same(endpoint, result);
        Assert.Equal("updated content", endpoint.Content);
        Assert.Equal("eng", endpoint.Language);
        Assert.Equal(string.Empty, endpoint.Genre); // Genre is ignored
        Assert.Equal("application/json", endpoint.FileContentType);
    }

    [Fact]
    public void FluentAPI_ComplexChaining_MaintainsCorrectState()
    {
        // Arrange & Act
        var endpoint = new TestContentEndpoint("original")
            .SetLanguage("eng")
            .SetContent("modified")
            .SetFileContentType("text/html");

        // Assert
        Assert.Equal("modified", endpoint.Content);
        Assert.Equal("eng", endpoint.Language);
        Assert.Equal("text/html", endpoint.FileContentType);
        Assert.Equal("test-endpoint", endpoint.Endpoint);
    }

    [Fact]
    public void SetContent_MultipleTypes_HandlesAllContentTypes()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("initial");

        // Act & Assert - String
        endpoint.SetContent("string content");
        Assert.Equal("string content", endpoint.Content);

        // Act & Assert - Uri
        var uri = new Uri("http://example.com");
        endpoint.SetContent(uri);
        Assert.Equal("http://example.com/", endpoint.Content.ToString());
    }

    [Fact]
    public void Constructor_WithFileStream_SetsContentAndFilename()
    {
        // Arrange
        string tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, "test data");
            using var fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read);

            // Act
            var endpoint = new TestContentEndpoint(fileStream);

            // Assert
            Assert.NotNull(endpoint.Content);
            Assert.Equal(tempFile, endpoint.Filename);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void SetLanguage_ISO6393Code_AcceptsThreeLetterCode()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        endpoint.SetLanguage("zho"); // Chinese

        // Assert
        Assert.Equal("zho", endpoint.Language);
    }

    [Fact]
    public void Options_SetOption_CanSetCustomOptions()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        endpoint.SetOption("customKey", "customValue");

        // Assert
        Assert.True(endpoint.Options.ContainsKey("customKey"));
        Assert.Equal("customValue", endpoint.Options["customKey"]);
    }

    [Fact]
    public void UrlParameters_SetUrlParameter_CanSetQueryParameters()
    {
        // Arrange
        var endpoint = new TestContentEndpoint("content");

        // Act
        endpoint.SetUrlParameter("output", "rosette");

        // Assert
        Assert.Equal("rosette", endpoint.UrlParameters["output"]);
    }
}