using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests.Endpoints;

public class SyntaxDependenciesTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "Yoshinori Ohsumi won the Nobel Prize.";
        SyntaxDependencies sd = new(text);

        Assert.Equal("syntax/dependencies", sd.Endpoint);
        Assert.Equal(text, sd.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        SyntaxDependencies sd = new SyntaxDependencies("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", sd.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        SyntaxDependencies sd = new SyntaxDependencies("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", sd.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        SyntaxDependencies sd = new SyntaxDependencies("Sample text")
            .SetFileContentType("text/plain");

        Assert.Equal("text/plain", sd.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        SyntaxDependencies sd = new SyntaxDependencies("The cat sat on the mat.")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("output", "rosette");

        Assert.Equal("eng", sd.Language);
        Assert.Equal("", sd.Genre);
        Assert.Equal("rosette", sd.Options["output"]);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/text");
        SyntaxDependencies sd = new(uri);

        Assert.Equal(uri, sd.Content);
    }

    [Fact]
    public void Constructor_AcceptsString_AsContent()
    {
        string text = "A complex sentence with multiple dependencies.";
        SyntaxDependencies sd = new(text);

        Assert.Equal(text, sd.Content);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        SyntaxDependencies sd = new SyntaxDependencies("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", sd.UrlParameters["output"]);
    }

    #endregion
}
