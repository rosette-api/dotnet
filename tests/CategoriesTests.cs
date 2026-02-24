using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class CategoriesTests
    {
        [Fact]
        public void CheckBasicUsage()
        {
            Categories c = new Categories("Sample text for categorization");
            
            Assert.Equal("categories", c.Endpoint);
            Assert.Equal("Sample text for categorization", c.Content);
        }

        [Fact]
        public void CheckWithLanguage()
        {
            Categories c = new Categories("Sample text")
                .SetLanguage("eng");
            
            Assert.Equal("eng", c.Language);
            Assert.Equal("Sample text", c.Content);
        }

        [Fact]
        public void CheckWithGenre()
        {
            Categories c = new Categories("Sample text")
                .SetGenre("social-media");
            
            Assert.Equal("", c.Genre);
        }

        [Fact]
        public void CheckFluentAPI()
        {
            Categories c = new Categories("Sample text")
                .SetLanguage("eng")
                .SetOption("customOption", "value");
            
            Assert.Equal("eng", c.Language);
            Assert.Equal("value", c.Options["customOption"]);
        }
    }
}