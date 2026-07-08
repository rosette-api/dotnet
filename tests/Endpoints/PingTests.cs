using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests.Endpoints;

public class PingTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpoint_WhenCalled()
    {
        Ping p = new();

        Assert.Equal("ping", p.Endpoint);
    }

    #endregion

    #region Endpoint Validation Tests

    [Fact]
    public void Endpoint_ReturnsCorrectValue_Always()
    {
        Ping p = new();

        Assert.Equal("ping", p.Endpoint);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        Ping p = new Ping()
            .SetOption("test", "value");

        Assert.Equal("value", p.Options["test"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        Ping p = new Ping()
            .SetOption("option1", "value1")
            .SetOption("option2", "value2");

        Assert.Equal("value1", p.Options["option1"]);
        Assert.Equal("value2", p.Options["option2"]);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        Ping p = new Ping()
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", p.UrlParameters["output"]);
    }

    [Fact]
    public void SetUrlParameter_SupportsMultipleParameters_WhenCalledMultipleTimes()
    {
        Ping p = new Ping()
            .SetUrlParameter("output", "rosette")
            .SetUrlParameter("format", "json");

        Assert.Equal("rosette", p.UrlParameters["output"]);
        Assert.Equal("json", p.UrlParameters["format"]);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingProperties()
    {
        Ping p = new Ping()
            .SetOption("test", "value")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("value", p.Options["test"]);
        Assert.Equal("rosette", p.UrlParameters["output"]);
    }

    #endregion

    #region Instance Creation Tests

    [Fact]
    public void Constructor_CreatesNewInstance_WhenCalled()
    {
        Ping p1 = new();
        Ping p2 = new();

        Assert.NotSame(p1, p2);
    }

    #endregion
}
