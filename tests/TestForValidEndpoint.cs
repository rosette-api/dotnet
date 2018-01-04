using Xunit;

namespace rosette_api.tests
{
    public class TestForValidEndpoint
    {
        [Fact]
        public void CategoriesEndpoint() {
            CategoriesEndpoint c = new CategoriesEndpoint("foo");

            Assert.Equal("categories", c.Endpoint);
            Assert.Equal("foo", c.Content);
        }
        [Fact]
        public void EntitiesEndpoint() {
            EntitiesEndpoint e = new EntitiesEndpoint("foo");
            Assert.Equal("entities", e.Endpoint);
            Assert.Equal("foo", e.Content);
        }

        [Fact]
        public void InfoEndpoint() {
            InfoEndpoint i = new InfoEndpoint();
            Assert.Equal("info", i.Endpoint);
        }

        [Fact]
        public void LanguageEndpoint() {
            LanguageEndpoint l = new LanguageEndpoint("foo");

            Assert.Equal("language", l.Endpoint);
            Assert.Equal("foo", l.Content);
        }

        [Theory]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.complete)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.compoundComponents)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.hanReadings)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.lemmas)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.partsOfSpeech)]
        public void MorphologyEndpoint(MorphologyEndpoint.MorphologyFeature feature) {
            MorphologyEndpoint m = new MorphologyEndpoint("foo", feature);

            Assert.Equal("morphology/" + m.FeatureAsString(feature), m.Endpoint);
            Assert.Equal("foo", m.Content);
        }

        [Fact]
        public void NameSimilarityEndpoint() {
            RosetteName rn = new RosetteName("foo");
            NameSimilarityEndpoint ns = new NameSimilarityEndpoint(rn, rn);
            Assert.Equal("name-similarity", ns.Endpoint);
        }

        [Fact]
        public void PingEndpoint() {
            PingEndpoint p = new PingEndpoint();
            Assert.Equal("ping", p.Endpoint);
        }

        [Fact]
        public void RelationshipsEndpoint() {
            RelationshipsEndpoint r = new RelationshipsEndpoint("foo");

            Assert.Equal("relationships", r.Endpoint);
            Assert.Equal("foo", r.Content);
        }

        [Fact]
        public void SentencesEndpoint() {
            SentencesEndpoint s = new SentencesEndpoint("foo");
            Assert.Equal("sentences", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void SentimentEndpoint() {
            SentimentEndpoint s = new SentimentEndpoint("foo");
            Assert.Equal("sentiment", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void SyntaxDependenciesEndpoint() {
            SyntaxDependenciesEndpoint s = new SyntaxDependenciesEndpoint("foo");

            Assert.Equal("syntax/dependencies", s.Endpoint);
            Assert.Equal("foo", s.Content);
        }

        [Fact]
        public void TextEmbeddingEndpoint() {
            TextEmbeddingEndpoint t = new TextEmbeddingEndpoint("foo");

            Assert.Equal("text-embedding", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }

        [Fact]
        public void TokensEndpoint() {
            TokensEndpoint t = new TokensEndpoint("foo");

            Assert.Equal("tokens", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }

        [Fact]
        public void TopicsEndpoint() {
            TopicsEndpoint t = new TopicsEndpoint("foo");

            Assert.Equal("topics", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }

        [Fact]
        public void TransliterationEndpoint() {
            TransliterationEndpoint t = new TransliterationEndpoint("foo");

            Assert.Equal("transliteration", t.Endpoint);
            Assert.Equal("foo", t.Content);
        }
    }
}
