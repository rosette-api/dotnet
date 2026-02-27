using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class LanguageTests
{
    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        Language l = new Language("Por favor Señorita, says the man.");

        Assert.Equal("language", l.Endpoint);
        Assert.Equal("Por favor Señorita, says the man.", l.Content);
    }

    [Fact]
    public void Constructor_SetsContentFromUri_WhenCalledWithUri()
    {
        Uri uri = new Uri("http://example.com");
        Language l = new Language(uri);

        Assert.Equal("http://example.com/", l.Content.ToString());
    }

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
}