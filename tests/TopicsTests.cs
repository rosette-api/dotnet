using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class TopicsTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "Lily Collins stars in the new Tolkien biopic.";
        Topics t = new(text);

        Assert.Equal("topics", t.Endpoint);
        Assert.Equal(text, t.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        Topics t = new Topics("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", t.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        Topics t = new Topics("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", t.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        Topics t = new Topics("Sample text")
            .SetFileContentType("text/plain");

        Assert.Equal("text/plain", t.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Topics t = new Topics("Article about climate change and renewable energy.")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("maxTopics", 5);

        Assert.Equal("eng", t.Language);
        Assert.Equal("", t.Genre);
        Assert.Equal(5, t.Options["maxTopics"]);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/article");
        Topics t = new(uri);

        Assert.Equal(uri, t.Content);
    }

    [Fact]
    public void Constructor_AcceptsString_AsContent()
    {
        string text = "Technology companies are investing in artificial intelligence.";
        Topics t = new(text);

        Assert.Equal(text, t.Content);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        Topics t = new Topics("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", t.UrlParameters["output"]);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        Topics t = new Topics("Sample text")
            .SetOption("maxTopics", 10);

        Assert.Equal(10, t.Options["maxTopics"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        Topics t = new Topics("Sample text")
            .SetOption("maxTopics", 10)
            .SetOption("minConfidence", 0.75);

        Assert.Equal(10, t.Options["maxTopics"]);
        Assert.Equal(0.75, t.Options["minConfidence"]);
    }

    #endregion
}
