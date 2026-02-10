using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class TestForValidEndpoint
    {
        [Fact]
        public void CategoriesEndpoint() {
            Categories c = new Categories("foo");

            Assert.Equal("categories", c.Endpoint);
            Assert.Equal("foo", c.Content);
        }
        [Fact]
        public void EntitiesEndpoint() {
            Entities e = new Entities("foo");
            Assert.Equal("entities", e.Endpoint);
            Assert.Equal("foo", e.Content);
        }

        [Fact]
        public void EventsEndpoint()
        {
            Events e = new Events("foo");
            Assert.Equal("events", e.Endpoint);
            Assert.Equal("foo", e.Content);
        }

        [Fact]
        public void InfoEndpoint() {
            Info i = new Info();
            Assert.Equal("info", i.Endpoint);
        }

        [Fact]
        public void LanguageEndpoint() {
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
        public void MorphologyEndpoint(MorphologyFeature feature) {
            Morphology m = new Morphology("foo", feature);

            Assert.Equal("morphology/" + m.FeatureAsString(feature), m.Endpoint);
            Assert.Equal("foo", m.Content);
        }

        [Fact]
        public void NameSimilarityEndpoint() {
            Name rn = new Name("foo");
            NameSimilarity ns = new NameSimilarity(rn, rn);
            Assert.Equal("name-similarity", ns.Endpoint);
        }

        [Fact]
        public void PingEndpoint() {
            Ping p = new Ping();
            Assert.Equal("ping", p.Endpoint);
        }

        [Fact]
        public void RelationshipsEndpoint() {
            Relationships r = new Relationships("foo");

            Assert.Equal("relationships", r.Endpoint);
            Assert.Equal("foo", r.Content);
        }

        [Fact]
        public void SemanticVectorsEndpoint()
        {
            SemanticsVector s = new SemanticsVector("foo");
            Assert.Equal("semantics/vector", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void SentencesEndpoint() {
            Sentences s = new Sentences("foo");
            Assert.Equal("sentences", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void SentimentEndpoint() {
            Sentiment s = new Sentiment("foo");
            Assert.Equal("sentiment", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void SyntaxDependenciesEndpoint() {
            SyntaxDependencies s = new SyntaxDependencies("foo");

            Assert.Equal("syntax/dependencies", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void TextEmbeddingEndpoint() {
            TextEmbedding t = new TextEmbedding("foo");

            Assert.Equal("text-embedding", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }

        [Fact]
        public void TokensEndpoint() {
            Tokens t = new Tokens("foo");

            Assert.Equal("tokens", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }

        [Fact]
        public void TopicsEndpoint() {
            Topics t = new Topics("foo");

            Assert.Equal("topics", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }

        [Fact]
        public void TransliterationEndpoint() {
            Transliteration t = new Transliteration("foo");

            Assert.Equal("transliteration", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }
    }
}
