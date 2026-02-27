using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class SentencesTests
{
    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "This is the first sentence. This is the second sentence.";
        Sentences s = new Sentences(text);

        Assert.Equal("sentences", s.Endpoint);
        Assert.Equal(text, s.Content);
    }

    [Fact]
    public void SetLanguage_SetsLanguageProperty_WhenCalled()
    {
        Sentences s = new Sentences("Sample text.")
            .SetLanguage("eng");

        Assert.Equal("eng", s.Language);
    }

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingLanguage()
    {
        Sentences s = new Sentences("Text content.")
            .SetLanguage("eng");

        Assert.Equal("eng", s.Language);
    }
}