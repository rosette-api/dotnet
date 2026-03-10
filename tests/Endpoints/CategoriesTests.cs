using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests.Endpoints;

public class CategoriesTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        Categories c = new("Sample text for categorization");

        Assert.Equal("categories", c.Endpoint);
        Assert.Equal("Sample text for categorization", c.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_SetsLanguageProperty_WhenCalled()
    {
        Categories c = new Categories("Sample text")
            .SetLanguage("eng");

        Assert.Equal("eng", c.Language);
        Assert.Equal("Sample text", c.Content);
    }

    [Fact]
    public void SetGenre_SetsGenreProperty_WhenCalled()
    {
        Categories c = new Categories("Sample text")
            .SetGenre("social-media");

        Assert.Equal("", c.Genre);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Categories c = new Categories("Sample text")
            .SetLanguage("eng")
            .SetOption("customOption", "value");

        Assert.Equal("eng", c.Language);
        Assert.Equal("value", c.Options["customOption"]);
    }

    #endregion
}
