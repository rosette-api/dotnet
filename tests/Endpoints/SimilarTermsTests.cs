using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests.Endpoints;

public class SimilarTermsTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
    {
        SimilarTerms st = new("happy");

        Assert.Equal("semantics/similar", st.Endpoint);
        Assert.Equal("happy", st.Content);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetLanguage_SetsLanguageProperty_WhenCalled()
    {
        SimilarTerms st = new SimilarTerms("computer")
            .SetLanguage("eng");

        Assert.Equal("eng", st.Language);
        Assert.Equal("computer", st.Content);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        SimilarTerms st = new SimilarTerms("innovation")
            .SetLanguage("eng")
            .SetOption("count", 10);

        Assert.Equal("eng", st.Language);
        Assert.Equal(10, st.Options["count"]);
    }

    #endregion
}
