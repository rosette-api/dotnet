using rosette_api;
using Xunit;

namespace tests {
    public class TestEndpointCommon
    {


        private static RosetteAPI Init() {
            return new RosetteAPI("testkey");
        }

        [Fact]
        public void CheckEndpoint() {
            EndpointCommon<EntitiesEndpoint> ec = new EndpointCommon<EntitiesEndpoint>("foo");
            Assert.Equal("foo", ec.Endpoint);
        }



        [Fact]
        public void CheckOptions() {
            EntitiesEndpoint e = new EntitiesEndpoint("foo").SetOption("test", "value");

            Assert.Equal("value", e.Options["test"]);

            e.SetOption("test2", "value2");

            Assert.Equal("value2", e.Options["test2"]);

            e.RemoveOption("value");

            Assert.True(!e.Options.ContainsKey("value"));

            e.ClearOptions();

            Assert.True(e.Options.Count == 0);
        }

        [Fact]
        public void CheckUrlParameters() {
            EntitiesEndpoint e = new EntitiesEndpoint("foo").SetUrlParameter("test", "value");

            Assert.Equal("value", e.UrlParameters["test"]);

            e.RemoveUrlParameter("test");

            Assert.True(e.UrlParameters.Count == 0);
        }



    }
}