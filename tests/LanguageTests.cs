using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class LanguageTests
    {
        [Fact]
        public void CheckBasicUsage()
        {
            Language l = new Language("Por favor Señorita, says the man.");
            
            Assert.Equal("language", l.Endpoint);
            Assert.Equal("Por favor Señorita, says the man.", l.Content);
        }

        [Fact]
        public void CheckWithUri()
        {
            Uri uri = new Uri("http://example.com");
            Language l = new Language(uri);
            
            Assert.Equal("http://example.com/", l.Content.ToString());
        }

        [Fact]
        public void CheckContentUpdate()
        {
            Language l = new Language("Original text")
                .SetContent("Updated text");
            
            Assert.Equal("Updated text", l.Content);
        }

        [Fact]
        public void CheckWithGenre()
        {
            Language l = new Language("Sample text")
                .SetGenre("social-media");
            
            Assert.Equal("", l.Genre);
        }
    }
}