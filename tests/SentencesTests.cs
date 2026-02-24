using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class SentencesTests
    {
        [Fact]
        public void CheckBasicUsage()
        {
            string text = "This is the first sentence. This is the second sentence.";
            Sentences s = new Sentences(text);
            
            Assert.Equal("sentences", s.Endpoint);
            Assert.Equal(text, s.Content);
        }

        [Fact]
        public void CheckWithLanguage()
        {
            Sentences s = new Sentences("Sample text.")
                .SetLanguage("eng");
            
            Assert.Equal("eng", s.Language);
        }

        [Fact]
        public void CheckFluentAPI()
        {
            Sentences s = new Sentences("Text content.")
                .SetLanguage("eng");
            
            Assert.Equal("eng", s.Language);
        }
    }
}