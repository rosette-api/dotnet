using Xunit;

namespace rosette_api.tests
{
    public class TestForValidEndpoint
    {
        [Fact]
        public void CategoriesEndpoint() {
            CategoriesEndpoint c = new CategoriesEndpoint();

            Assert.Equal("categories", c.Endpoint);
        }
        [Fact]
        public void EntitiesEndpoint() {
            EntitiesEndpoint e = new EntitiesEndpoint();
            Assert.Equal("entities", e.Endpoint);
        }

        [Fact]
        public void InfoEndpoint() {
            InfoEndpoint i = new InfoEndpoint();
            Assert.Equal("info", i.Endpoint);
        }

        [Fact]
        public void LanguageEndpoint() {
            LanguageEndpoint l = new LanguageEndpoint();

            Assert.Equal("language", l.Endpoint);
        }

        [Theory]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.complete)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.compoundComponents)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.hanReadings)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.lemmas)]
        [InlineData(rosette_api.MorphologyEndpoint.MorphologyFeature.partsOfSpeech)]
        public void MorphologyEndpoint(MorphologyEndpoint.MorphologyFeature feature) {
            MorphologyEndpoint m = new MorphologyEndpoint(feature);

            Assert.Equal("morphology/" + m.FeatureAsString(feature), m.Endpoint);
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
            RelationshipsEndpoint r = new RelationshipsEndpoint();

            Assert.Equal("relationships", r.Endpoint);
        }

        [Fact]
        public void SentencesEndpoint() {
            SentencesEndpoint s = new SentencesEndpoint();
            Assert.Equal("sentences", s.Endpoint);
        }

        [Fact]
        public void SentimentEndpoint() {
            SentimentEndpoint s = new SentimentEndpoint();
            Assert.Equal("sentiment", s.Endpoint);
        }

        [Fact]
        public void SyntaxDependenciesEndpoint() {
            SyntaxDependenciesEndpoint s = new SyntaxDependenciesEndpoint();

            Assert.Equal("syntax/dependencies", s.Endpoint);
        }

        [Fact]
        public void TextEmbeddingEndpoint() {
            TextEmbeddingEndpoint t = new TextEmbeddingEndpoint();

            Assert.Equal("text-embedding", t.Endpoint);
        }

        [Fact]
        public void TokensEndpoint() {
            TokensEndpoint t = new TokensEndpoint();

            Assert.Equal("tokens", t.Endpoint);
        }

        [Fact]
        public void TopicsEndpoint() {
            TopicsEndpoint t = new TopicsEndpoint();

            Assert.Equal("topics", t.Endpoint);
        }

        [Fact]
        public void TransliterationEndpoint() {
            TransliterationEndpoint t = new TransliterationEndpoint();

            Assert.Equal("transliteration", t.Endpoint);
        }
    }
}
