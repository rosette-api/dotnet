using Rosette.Api.Endpoints;
using Rosette.Api.Models;

namespace Rosette.Api.Tests;

public class ValidEndpointTests
{

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingAddressSimilarity()
    {
        var a = new UnfieldedAddressRecord { Address = "foo" };

        AddressSimilarity asim = new AddressSimilarity(a,a);
        Assert.Equal("address-similarity", asim.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingCategories()
    {
        Categories c = new Categories("foo");

        Assert.Equal("categories", c.Endpoint);
        Assert.Equal("foo", c.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingEntities()
    {
        Entities e = new Entities("foo");
        Assert.Equal("entities", e.Endpoint);
        Assert.Equal("foo", e.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingEvents()
    {
        Events e = new Events("foo");
        Assert.Equal("events", e.Endpoint);
        Assert.Equal("foo", e.Content);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingInfo()
    {
        Info i = new Info();
        Assert.Equal("info", i.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingLanguage()
    {
        Language l = new Language("foo");

        Assert.Equal("language", l.Endpoint);
        Assert.Equal("foo", l.Content);
    }

    [Theory]
    [InlineData(MorphologyFeature.complete)]
    [InlineData(MorphologyFeature.compoundComponents)]
    [InlineData(MorphologyFeature.hanReadings)]
    [InlineData(MorphologyFeature.lemmas)]
    [InlineData(MorphologyFeature.partsOfSpeech)]
    public void Constructor_SetsEndpointAndContent_WhenCreatingMorphologyWithFeature(MorphologyFeature feature)
    {
        Morphology m = new Morphology("foo", feature);

        Assert.Equal("morphology/" + m.FeatureAsString(feature), m.Endpoint);
        Assert.Equal("foo", m.Content);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingNameDeduplication()
    {
        List<Name> names = [
            new Name("foo"),
            new Name("bar")
        ];
        NameDeduplication nd = new NameDeduplication(names);

        Assert.Equal("name-deduplication", nd.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingNameSimilarity()
    {
        Name rn = new Name("foo");
        NameSimilarity ns = new NameSimilarity(rn, rn);
        Assert.Equal("name-similarity", ns.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingNameTranslation()
    {
        NameTranslation nt = new NameTranslation("foo");

        Assert.Equal("name-translation", nt.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingPing()
    {
        Ping p = new Ping();
        Assert.Equal("ping", p.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpoint_WhenCreatingRecordSimilarity()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new RecordSimilarity(fields, properties, records);

        Assert.Equal("record-similarity", rs.Endpoint);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingRelationships()
    {
        Relationships r = new Relationships("foo");

        Assert.Equal("relationships", r.Endpoint);
        Assert.Equal("foo", r.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSemanticsVector()
    {
        SemanticsVector s = new SemanticsVector("foo");
        Assert.Equal("semantics/vector", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSentences()
    {
        Sentences s = new Sentences("foo");
        Assert.Equal("sentences", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSentiment()
    {
        Sentiment s = new Sentiment("foo");
        Assert.Equal("sentiment", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSimilarTerms()
    {
        SimilarTerms st = new SimilarTerms("foo");

        Assert.Equal("semantics/similar", st.Endpoint);
        Assert.Equal("foo", st.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingSyntaxDependencies()
    {
        SyntaxDependencies s = new SyntaxDependencies("foo");

        Assert.Equal("syntax/dependencies", s.Endpoint);
        Assert.Equal("foo", s.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTextEmbedding()
    {
        TextEmbedding t = new TextEmbedding("foo");

        Assert.Equal("text-embedding", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTokens()
    {
        Tokens t = new Tokens("foo");

        Assert.Equal("tokens", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTopics()
    {
        Topics t = new Topics("foo");

        Assert.Equal("topics", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }

    [Fact]
    public void Constructor_SetsEndpointAndContent_WhenCreatingTransliteration()
    {
        Transliteration t = new Transliteration("foo");

        Assert.Equal("transliteration", t.Endpoint);
        Assert.Equal("foo", t.Content);
    }
}