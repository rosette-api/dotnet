using rosette_api;
using Xunit;

namespace tests
{
    public class TestNameDeduplication
    {
        [Fact]
        public void CheckBasicUsage() {
            List<RosetteName> names = new List<RosetteName> {
                new RosetteName("foo"),
                new RosetteName("bar")
            };
            NameDeduplicationEndpoint n = new NameDeduplicationEndpoint(names);
            Assert.Equal(names, n.Names);
            Assert.Equal(0.75f, n.Threshold);
        }

        [Fact]
        public void CheckProfileID() {
            List<RosetteName> names = new List<RosetteName> {
                new RosetteName("foo"),
                new RosetteName("bar")
            };
            NameDeduplicationEndpoint n = new NameDeduplicationEndpoint(names).SetProfileID("profileid");
            Assert.Equal(names, n.Names);
            Assert.Equal("profileid", n.ProfileID);
        }

        [Fact]
        public void CheckThreshold() {
            List<RosetteName> names = new List<RosetteName> {
                new RosetteName("foo"),
                new RosetteName("bar")
            };
            NameDeduplicationEndpoint n = new NameDeduplicationEndpoint(names).SetThreshold(0.8f);
            Assert.Equal(names, n.Names);
            Assert.Equal(0.8f, n.Threshold);
        }
    }
}
