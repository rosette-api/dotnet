using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests.Endpoints;

public class TextEmbeddingTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "Cambridge, Massachusetts";
        TextEmbedding te = new(text);

        Assert.Equal("text-embedding", te.Endpoint);
        Assert.Equal(text, te.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", te.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", te.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetFileContentType("text/plain");

        Assert.Equal("text/plain", te.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        TextEmbedding te = new TextEmbedding("Boston, Massachusetts")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("model", "multilingual");

        Assert.Equal("eng", te.Language);
        Assert.Equal("", te.Genre);
        Assert.Equal("multilingual", te.Options["model"]);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/text");
        TextEmbedding te = new(uri);

        Assert.Equal(uri, te.Content);
    }

    [Fact]
    public void Constructor_AcceptsString_AsContent()
    {
        string text = "New York City";
        TextEmbedding te = new(text);

        Assert.Equal(text, te.Content);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", te.UrlParameters["output"]);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetOption("model", "english");

        Assert.Equal("english", te.Options["model"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetOption("model", "multilingual")
            .SetOption("pooling", "mean");

        Assert.Equal("multilingual", te.Options["model"]);
        Assert.Equal("mean", te.Options["pooling"]);
    }

    #endregion

    #region Model-Specific Tests

    [Fact]
    public void SetOption_AcceptsMultilingualModel_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetOption("model", "multilingual");

        Assert.Equal("multilingual", te.Options["model"]);
    }

    [Fact]
    public void SetOption_AcceptsEnglishModel_WhenCalled()
    {
        TextEmbedding te = new TextEmbedding("Sample text")
            .SetOption("model", "english");

        Assert.Equal("english", te.Options["model"]);
    }

    #endregion
}
