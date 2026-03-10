using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests
{
    public class RelationshipsTests
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_SetsEndpointAndContent_WhenCalledWithText()
        {
            string text = "John works at Microsoft.";
            Relationships r = new(text);

            Assert.Equal("relationships", r.Endpoint);
            Assert.Equal(text, r.Content);
        }

        #endregion

        #region Property Configuration Tests

        [Fact]
        public void SetLanguage_SetsLanguageProperty_WhenCalled()
        {
            Relationships r = new Relationships("Sample text")
                .SetLanguage("eng");

            Assert.Equal("eng", r.Language);
        }

        #endregion

        #region Fluent API Tests

        [Fact]
        public void FluentAPI_AllowsMethodChaining_WhenSettingMultipleProperties()
        {
            Relationships r = new Relationships("Sample relationship text.")
                .SetLanguage("eng")
                .SetOption("accuracy", "high");

            Assert.Equal("eng", r.Language);
            Assert.Equal("high", r.Options["accuracy"]);
        }

        #endregion
    }
}
