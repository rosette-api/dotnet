using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class TransliterationTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "ana r2ye7 el gam3a el sa3a 3 el 3asr";
        Transliteration tr = new(text);

        Assert.Equal("transliteration", tr.Endpoint);
        Assert.Equal(text, tr.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        Transliteration tr = new Transliteration("Sample text")
            .SetLanguage("ara");

        Assert.Equal("ara", tr.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        Transliteration tr = new Transliteration("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", tr.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        Transliteration tr = new Transliteration("Sample text")
            .SetFileContentType("text/plain");

        Assert.Equal("text/plain", tr.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Transliteration tr = new Transliteration("ana r2ye7 el gam3a")
            .SetLanguage("ara")
            .SetGenre("social-media")
            .SetOption("romanization", "native");

        Assert.Equal("ara", tr.Language);
        Assert.Equal("", tr.Genre);
        Assert.Equal("native", tr.Options["romanization"]);
    }

    #endregion

    #region Language-Specific Tests

    [Fact]
    public void SetLanguage_AcceptsArabic_WhenCalled()
    {
        Transliteration tr = new Transliteration("arabic text")
            .SetLanguage("ara");

        Assert.Equal("ara", tr.Language);
    }

    [Fact]
    public void SetLanguage_AcceptsJapanese_WhenCalled()
    {
        Transliteration tr = new Transliteration("japanese text")
            .SetLanguage("jpn");

        Assert.Equal("jpn", tr.Language);
    }

    [Fact]
    public void SetLanguage_AcceptsChinese_WhenCalled()
    {
        Transliteration tr = new Transliteration("chinese text")
            .SetLanguage("zho");

        Assert.Equal("zho", tr.Language);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/text");
        Transliteration tr = new(uri);

        Assert.Equal(uri, tr.Content);
    }

    [Fact]
    public void Constructor_AcceptsString_AsContent()
    {
        string text = "Text to transliterate";
        Transliteration tr = new(text);

        Assert.Equal(text, tr.Content);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        Transliteration tr = new Transliteration("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", tr.UrlParameters["output"]);
    }

    #endregion
}
