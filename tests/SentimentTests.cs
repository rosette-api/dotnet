using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class SentimentTests
{
    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        Sentiment s = new Sentiment("This is a great product!");

        Assert.Equal("sentiment", s.Endpoint);
        Assert.Equal("This is a great product!", s.Content);
    }

    [Fact]
    public void SetLanguage_SetsLanguageProperty_WhenCalled()
    {
        Sentiment s = new Sentiment("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", s.Language);
        Assert.Equal("Sample text", s.Content);
    }

    [Fact]
    public void SetGenre_SetsGenreProperty_WhenCalled()
    {
        Sentiment s = new Sentiment("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", s.Genre);
    }

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Sentiment s = new Sentiment("Great service!")
            .SetLanguage("eng")
            .SetOption("sentiment.threshold", 0.5);

        Assert.Equal("eng", s.Language);
        Assert.Equal(0.5, s.Options["sentiment.threshold"]);
    }
}