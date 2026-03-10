using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class LanguageTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        Language l = new("Por favor Señorita, says the man.");

        Assert.Equal("language", l.Endpoint);
        Assert.Equal("Por favor Señorita, says the man.", l.Content);
    }

    [Fact]
    public void Constructor_SetsContentFromUri_WhenCalledWithUri()
    {
        Uri uri = new("http://example.com");
        Language l = new(uri);

        Assert.Equal("http://example.com/", l.Content.ToString());
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetContent_UpdatesContent_WhenCalled()
    {
        Language l = new Language("Original text")
            .SetContent("Updated text");

        Assert.Equal("Updated text", l.Content);
    }

    [Fact]
    public void SetGenre_SetsGenreProperty_WhenCalled()
    {
        Language l = new Language("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", l.Genre);
    }

    #endregion
}