using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class MorphologyTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCalledWithDefaultFeature()
    {
        string text = "The quick brown fox jumped over the lazy dog.";
        Morphology m = new(text);

        Assert.Equal("morphology/complete", m.Endpoint);
        Assert.Equal(text, m.Content);
    }

    [Theory]
    [InlineData(MorphologyFeature.complete, "morphology/complete")]
    [InlineData(MorphologyFeature.lemmas, "morphology/lemmas")]
    [InlineData(MorphologyFeature.partsOfSpeech, "morphology/parts-of-speech")]
    [InlineData(MorphologyFeature.compoundComponents, "morphology/compound-components")]
    [InlineData(MorphologyFeature.hanReadings, "morphology/han-readings")]
    public void Constructor_SetsCorrectEndpoint_ForEachFeature(MorphologyFeature feature, string expectedEndpoint)
    {
        Morphology m = new("Sample text", feature);

        Assert.Equal(expectedEndpoint, m.Endpoint);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetContent_UpdatesContent_WhenCalled()
    {
        Morphology m = new("initial content");
        string newContent = "updated content";

        m.SetContent(newContent);

        Assert.Equal(newContent, m.Content);
    }

    [Fact]
    public void SetLanguage_UpdatesLanguage_WhenCalled()
    {
        Morphology m = new("Sample text");
        string language = "eng";

        m.SetLanguage(language);

        Assert.Equal("eng", m.Language);
    }

    [Fact]
    public void SetGenre_UpdatesGenre_WhenCalled()
    {
        Morphology m = new("Sample text");
        string genre = "social-media";

        m.SetGenre(genre);

        Assert.Equal("social-media", m.Genre);
    }

    [Fact]
    public void SetFileContentType_UpdatesFileContentType_WhenCalled()
    {
        Morphology m = new("Sample text");
        string fileContentType = "text/plain";

        m.SetFileContentType(fileContentType);

        Assert.Equal("text/plain", m.FileContentType);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
    {
        Morphology m = new Morphology("The geese went back to get a rest", MorphologyFeature.lemmas)
            .SetLanguage("eng")
            .SetGenre("news")
            .SetOption("debug", true);

        Assert.Equal("morphology/lemmas", m.Endpoint);
        Assert.Equal("eng", m.Language);
        Assert.Equal("news", m.Genre);
        Assert.True((bool)m.Options["debug"]);
    }

    #endregion

    #region Feature-Specific Tests

    [Fact]
    public void Constructor_WithLemmasFeature_SetsLemmasEndpoint()
    {
        Morphology m = new("banking on their return", MorphologyFeature.lemmas);

        Assert.Equal("morphology/lemmas", m.Endpoint);
    }

    [Fact]
    public void Constructor_WithPartsOfSpeechFeature_SetsPartsOfSpeechEndpoint()
    {
        Morphology m = new("The cat sat on the mat", MorphologyFeature.partsOfSpeech);

        Assert.Equal("morphology/parts-of-speech", m.Endpoint);
    }

    [Fact]
    public void Constructor_WithCompoundComponentsFeature_SetsCompoundComponentsEndpoint()
    {
        Morphology m = new("Rechtsschutzversicherungsgesellschaften", MorphologyFeature.compoundComponents);

        Assert.Equal("morphology/compound-components", m.Endpoint);
    }

    [Fact]
    public void Constructor_WithHanReadingsFeature_SetsHanReadingsEndpoint()
    {
        Morphology m = new("北京大学生物系主任办公室内部会议", MorphologyFeature.hanReadings);

        Assert.Equal("morphology/han-readings", m.Endpoint);
    }

    #endregion

    #region Content Type Tests

    [Fact]
    public void Constructor_AcceptsUri_AsContent()
    {
        Uri uri = new("https://example.com/text");
        Morphology m = new(uri, MorphologyFeature.complete);

        Assert.Equal(uri, m.Content);
    }

    #endregion
}
