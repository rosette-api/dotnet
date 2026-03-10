using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class ValidEndpointTests
{
    #region Address and Record Endpoints

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingAddressSimilarity()
    {
        var a = new UnfieldedAddressRecord { Address = "foo" };

        AddressSimilarity asim = new(a,a);
        Assert.Equal("address-similarity", asim.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingRecordSimilarity()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        Assert.Equal("record-similarity", rs.Endpoint);
    }

    #endregion

    #region Content Analysis Endpoints

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingCategories()
    {
        Categories c = new("foo");

        Assert.Equal("categories", c.Endpoint);
        Assert.Equal("foo", c.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingEntities()
    {
        Entities e = new("foo");
        Assert.Equal("entities", e.Endpoint);
        Assert.Equal("foo", e.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingEvents()
    {
        Events e = new("foo");
        Assert.Equal("events", e.Endpoint);
        Assert.Equal("foo", e.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingRelationships()
    {
        Relationships r = new("foo");

        Assert.Equal("relationships", r.Endpoint);
        Assert.Equal("foo", r.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSentiment()
    {
        Sentiment s = new("foo");
        Assert.Equal("sentiment", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTopics()
    {
        Topics t = new("foo");

        Assert.Equal("topics", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    #endregion

    #region Language and Text Processing Endpoints

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingLanguage()
    {
        Language l = new("foo");

        Assert.Equal("language", l.Endpoint);
        Assert.Equal("foo", l.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSentences()
    {
        Sentences s = new("foo");
        Assert.Equal("sentences", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTokens()
    {
        Tokens t = new("foo");

        Assert.Equal("tokens", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTransliteration()
    {
        Transliteration t = new("foo");

        Assert.Equal("transliteration", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    #endregion

    #region Morphology Endpoints

    [Theory]
    [InlineData(MorphologyFeature.complete)]
    [InlineData(MorphologyFeature.compoundComponents)]
    [InlineData(MorphologyFeature.hanReadings)]
    [InlineData(MorphologyFeature.lemmas)]
    [InlineData(MorphologyFeature.partsOfSpeech)]
    public void Constructor_SetsEndpointAndContent_WhenCreatingMorphologyWithFeature(MorphologyFeature feature)
    {
        Morphology m = new("foo", feature);

        Assert.Equal("morphology/" + m.FeatureAsString(feature), m.Endpoint);
        Assert.Equal("foo", m.Content);
    }

    #endregion

    #region Name Endpoints

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingNameDeduplication()
    {
        List<Name> names = [
            new Name("foo"),
            new Name("bar")
        ];
        NameDeduplication nd = new(names);

        Assert.Equal("name-deduplication", nd.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingNameSimilarity()
    {
        Name rn = new("foo");
        NameSimilarity ns = new(rn, rn);
        Assert.Equal("name-similarity", ns.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingNameTranslation()
    {
        NameTranslation nt = new("foo");

        Assert.Equal("name-translation", nt.Endpoint);
    }

    #endregion

    #region Semantics Endpoints

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSemanticsVector()
    {
        SemanticsVector s = new("foo");
        Assert.Equal("semantics/vector", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSimilarTerms()
    {
        SimilarTerms st = new("foo");

        Assert.Equal("semantics/similar", st.Endpoint);
        Assert.Equal("foo", st.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTextEmbedding()
    {
        TextEmbedding t = new("foo");

        Assert.Equal("text-embedding", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    #endregion

    #region Syntax Endpoints

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSyntaxDependencies()
    {
        SyntaxDependencies s = new("foo");

        Assert.Equal("syntax/dependencies", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    #endregion

    #region Utility Endpoints

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingInfo()
    {
        Info i = new();
        Assert.Equal("info", i.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingPing()
    {
        Ping p = new();
        Assert.Equal("ping", p.Endpoint);
    }

    #endregion
}
