using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests;

public class NameTranslationTests
{
    [Fact]
    public void CheckBasicUsage() {
        NameTranslation n = new NameTranslation("foo");
        Assert.Equal("foo", n.Name);
        Assert.Equal("eng", n.TargetLanguage);
        Assert.Empty(n.EntityType);
        Assert.Empty(n.SourceLanguageOfOrigin);
        Assert.Empty(n.SourceLanguageOfUse);
        Assert.Empty(n.SourceScript);
        Assert.Empty(n.TargetScheme);
        Assert.Empty(n.TargetScript);
    }

    [Fact]
    public void CheckAllUsage() {
        NameTranslation n = new NameTranslation("foo")
            .SetEntityType("PERSON")
            .SetSourceLanguageOfOrigin("eng")
            .SetSourceLanguageOfUse("eng")
            .SetSourceScript("zho")
            .SetTargetLanguage("spa")
            .SetTargetScheme("BGN")
            .SetTargetScript("eng");
        Assert.Equal("foo", n.Name);
        Assert.Equal("spa", n.TargetLanguage);
        Assert.Equal("PERSON", n.EntityType);
        Assert.Equal("eng", n.SourceLanguageOfOrigin);
        Assert.Equal("eng", n.SourceLanguageOfUse);
        Assert.Equal("zho", n.SourceScript);
        Assert.Equal("BGN", n.TargetScheme);
        Assert.Equal("eng", n.TargetScript);
    }

}
