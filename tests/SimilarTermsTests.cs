using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class SimilarTermsTests
    {
        [Fact]
        public void CheckBasicUsage()
        {
            SimilarTerms st = new SimilarTerms("happy");
            
            Assert.Equal("semantics/similar", st.Endpoint);
            Assert.Equal("happy", st.Content);
        }

        [Fact]
        public void CheckWithLanguage()
        {
            SimilarTerms st = new SimilarTerms("computer")
                .SetLanguage("eng");
            
            Assert.Equal("eng", st.Language);
            Assert.Equal("computer", st.Content);
        }

        [Fact]
        public void CheckFluentAPI()
        {
            SimilarTerms st = new SimilarTerms("innovation")
                .SetLanguage("eng")
                .SetOption("count", 10);
            
            Assert.Equal("eng", st.Language);
            Assert.Equal(10, st.Options["count"]);
        }
    }
}