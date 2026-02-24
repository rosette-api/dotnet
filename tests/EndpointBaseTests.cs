using Rosette.Api.Endpoints;
using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Tests {
    public class EndpointBaseTests
    {


        private static ApiClient Init() {
            return new ApiClient("testkey");
        }

        [Fact]
        public void CheckEndpoint() {
            EndpointBase<Entities> ec = new EndpointBase<Entities>("foo");
            Assert.Equal("foo", ec.Endpoint);
        }



        [Fact]
        public void CheckOptions() {
            Entities e = new Entities("foo").SetOption("test", "value");

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
            Entities e = new Entities("foo").SetUrlParameter("test", "value");

            Assert.Equal("value", e.UrlParameters["test"]);

            e.RemoveUrlParameter("test");

            Assert.True(e.UrlParameters.Count == 0);
        }



    }
}