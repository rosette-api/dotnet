using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class EntitiesTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        string text = "Bill Murray will appear in new Ghostbusters film.";
        Entities e = new(text);

        Assert.Equal("entities", e.Endpoint);
        Assert.Equal(text, e.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguageAndGenre_SetsProperties_WhenCalled()
    {
        Entities e = new Entities("Sample text")
            .SetLanguage("eng")
            .SetGenre("social-media");

        Assert.Equal("eng", e.Language);
        Assert.Equal("", e.Genre);
    }

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        Entities e = new Entities("Sample text")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", e.UrlParameters["output"]);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Entities e = new Entities("Apple announced a new iPhone.")
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("linkEntities", true)
            .SetUrlParameter("output", "rosette");

        Assert.Equal("eng", e.Language);
        Assert.True((bool)e.Options["linkEntities"]);
        Assert.Equal("rosette", e.UrlParameters["output"]);
    }

    #endregion
}
