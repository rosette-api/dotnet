using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class SentimentTests
    {
        [Fact]
        public void CheckBasicUsage()
        {
            Sentiment s = new Sentiment("This is a great product!");
            
            Assert.Equal("sentiment", s.Endpoint);
            Assert.Equal("This is a great product!", s.Content);
        }

        [Fact]
        public void CheckWithLanguage()
        {
            Sentiment s = new Sentiment("Sample text")
                .SetLanguage("eng");
            
            Assert.Equal("eng", s.Language);
            Assert.Equal("Sample text", s.Content);
        }

        [Fact]
        public void CheckWithGenre()
        {
            Sentiment s = new Sentiment("Sample text")
                .SetGenre("social-media");
            
            Assert.Equal("social-media", s.Genre);
        }

        [Fact]
        public void CheckFluentAPI()
        {
            Sentiment s = new Sentiment("Great service!")
                .SetLanguage("eng")
                .SetGenre("review")
                .SetOption("sentiment.threshold", 0.5);
            
            Assert.Equal("eng", s.Language);
            Assert.Equal("review", s.Genre);
            Assert.Equal(0.5, s.Options["sentiment.threshold"]);
        }
    }
}