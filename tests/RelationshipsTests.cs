using Rosette.Api.Endpoints;

namespace Rosette.Api.Tests
{
    public class RelationshipsTests
    {
        [Fact]
        public void CheckBasicUsage()
        {
            string text = "John works at Microsoft.";
            Relationships r = new Relationships(text);
            
            Assert.Equal("relationships", r.Endpoint);
            Assert.Equal(text, r.Content);
        }

        [Fact]
        public void CheckWithLanguage()
        {
            Relationships r = new Relationships("Sample text")
                .SetLanguage("eng");
            
            Assert.Equal("eng", r.Language);
        }

        [Fact]
        public void CheckFluentAPI()
        {
            Relationships r = new Relationships("Sample relationship text.")
                .SetLanguage("eng")
                .SetOption("accuracy", "high");
            
            Assert.Equal("eng", r.Language);
            Assert.Equal("high", r.Options["accuracy"]);
        }
    }
}