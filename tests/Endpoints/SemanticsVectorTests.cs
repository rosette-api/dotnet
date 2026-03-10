using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests.Endpoints;

public class SemanticsVectorTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "Cambridge, Massachusetts";
        SemanticsVector sv = new(text);

        Assert.Equal("semantics/vector", sv.Endpoint);
        Assert.Equal(text, sv.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        SemanticsVector sv = new SemanticsVector("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", sv.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        SemanticsVector sv = new SemanticsVector("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", sv.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        SemanticsVector sv = new SemanticsVector("Sample text")
            .SetFileContentType("text/plain");

        Assert.Equal("text/plain", sv.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        SemanticsVector sv = new SemanticsVector("New York City")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("vectorType", "embedding");

        Assert.Equal("eng", sv.Language);
        Assert.Equal("", sv.Genre);
        Assert.Equal("embedding", sv.Options["vectorType"]);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/text");
        SemanticsVector sv = new(uri);

        Assert.Equal(uri, sv.Content);
    }

    [Fact]
    public void Constructor_AcceptsString_AsContent()
    {
        string text = "San Francisco, California";
        SemanticsVector sv = new(text);

        Assert.Equal(text, sv.Content);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        SemanticsVector sv = new SemanticsVector("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", sv.UrlParameters["output"]);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        SemanticsVector sv = new SemanticsVector("Sample text")
            .SetOption("vectorType", "dense");

        Assert.Equal("dense", sv.Options["vectorType"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        SemanticsVector sv = new SemanticsVector("Sample text")
            .SetOption("vectorType", "dense")
            .SetOption("normalize", true);

        Assert.Equal("dense", sv.Options["vectorType"]);
        Assert.True((bool)sv.Options["normalize"]);
    }

    #endregion
}
