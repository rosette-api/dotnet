using rosette_api;
using Xunit;

namespace tests
{
    public class TestNameSimilarity
    {
        [Fact]
        public void CheckForNull() {
            var exception = Record.Exception(() => new NameSimilarityEndpoint(null, null));
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Contains("Name1 cannot be null", exception.Message);

            RosetteName rn = new RosetteName("foo");
            exception = Record.Exception(() => new NameSimilarityEndpoint(rn, null));
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Contains("Name2 cannot be null", exception.Message);
        }
    }
}
