using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class EventsTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "Bill Gates went to the store.";
        Events e = new(text);

        Assert.Equal("events", e.Endpoint);
        Assert.Equal(text, e.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        Events e = new Events("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", e.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        Events e = new Events("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", e.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        Events e = new Events("Sample text")
            .SetFileContentType("text/plain");

        Assert.Equal("text/plain", e.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Events e = new Events("The CEO announced a merger yesterday.")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("output", "rosette");

        Assert.Equal("eng", e.Language);
        Assert.Equal("", e.Genre);
        Assert.Equal("rosette", e.Options["output"]);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/article");
        Events e = new(uri);

        Assert.Equal(uri, e.Content);
    }

    [Fact]
    public void Constructor_AcceptsString_AsContent()
    {
        string text = "The company launched a new product last week.";
        Events e = new(text);

        Assert.Equal(text, e.Content);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        Events e = new Events("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", e.UrlParameters["output"]);
    }

    #endregion

    #region Negation Option Tests

    [Fact]
    public void SetOption_SupportsNegationOption_WhenCalledWithOnlyPositive()
    {
        Events e = new Events("Bill Gates did not go to the store.")
            .SetOption("negation", "ONLY_POSITIVE");

        Assert.Equal("ONLY_POSITIVE", e.Options["negation"]);
    }

    [Fact]
    public void SetOption_SupportsNegationOption_WhenCalledWithOnlyNegative()
    {
        Events e = new Events("Bill Gates did not go to the store.")
            .SetOption("negation", "ONLY_NEGATIVE");

        Assert.Equal("ONLY_NEGATIVE", e.Options["negation"]);
    }

    [Fact]
    public void SetOption_SupportsNegationOption_WhenCalledWithBoth()
    {
        Events e = new Events("Bill Gates did not go to the store.")
            .SetOption("negation", "BOTH");

        Assert.Equal("BOTH", e.Options["negation"]);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        Events e = new Events("Sample text")
            .SetOption("negation", "ONLY_POSITIVE")
            .SetOption("output", "rosette");

        Assert.Equal("ONLY_POSITIVE", e.Options["negation"]);
        Assert.Equal("rosette", e.Options["output"]);
    }

    #endregion
}
