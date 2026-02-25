using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests;

public class TokensTests
{
    [Fact]
    public void CheckBasicUsage()
    {
        Tokens t = new Tokens("This is sample text");
        
        Assert.Equal("tokens", t.Endpoint);
        Assert.Equal("This is sample text", t.Content);
    }

    [Fact]
    public void CheckWithLanguage()
    {
        Tokens t = new Tokens("Sample text")
            .SetLanguage("eng");
        
        Assert.Equal("eng", t.Language);
    }

    [Fact]
    public void CheckFluentAPI()
    {
        Tokens t = new Tokens("Sample text")
            .SetLanguage("jpn");
        
        Assert.Equal("jpn", t.Language);
    }
}