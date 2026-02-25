using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests;

public class EntitiesTests
{
    [Fact]
    public void CheckBasicUsage()
    {
        string text = "Bill Murray will appear in new Ghostbusters film.";
        Entities e = new Entities(text);
        
        Assert.Equal("entities", e.Endpoint);
        Assert.Equal(text, e.Content);
    }

    [Fact]
    public void CheckWithLanguageAndGenre()
    {
        Entities e = new Entities("Sample text")
            .SetLanguage("eng")
            .SetGenre("social-media");
        
        Assert.Equal("eng", e.Language);
        Assert.Equal("", e.Genre);
    }

    [Fact]
    public void CheckWithUrlParameter()
    {
        Entities e = new Entities("Sample text")
            .SetUrlParameter("output", "rosette");
        
        Assert.Equal("rosette", e.UrlParameters["output"]);
    }

    [Fact]
    public void CheckFluentAPI()
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
}