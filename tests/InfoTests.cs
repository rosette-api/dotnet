using Rosette.Api.Client.Endpoints;

namespace Rosette.Api.Tests;

public class InfoTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpoint_WhenCalled()
    {
        Info i = new();

        Assert.Equal("info", i.Endpoint);
    }

    #endregion

    #region Endpoint Validation Tests

    [Fact]
    public void Endpoint_ReturnsCorrectValue_Always()
    {
        Info i = new();

        Assert.Equal("info", i.Endpoint);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        Info i = new Info()
            .SetOption("test", "value");

        Assert.Equal("value", i.Options["test"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        Info i = new Info()
            .SetOption("option1", "value1")
            .SetOption("option2", "value2");

        Assert.Equal("value1", i.Options["option1"]);
        Assert.Equal("value2", i.Options["option2"]);
    }

    #endregion

    #region URL Parameter Tests

    [Fact]
    public void SetUrlParameter_AddsParameterToUrlParameters_WhenCalled()
    {
        Info i = new Info()
            .SetUrlParameter("output", "rosette");

        Assert.Equal("rosette", i.UrlParameters["output"]);
    }

    [Fact]
    public void SetUrlParameter_SupportsMultipleParameters_WhenCalledMultipleTimes()
    {
        Info i = new Info()
            .SetUrlParameter("output", "rosette")
            .SetUrlParameter("format", "json");

        Assert.Equal("rosette", i.UrlParameters["output"]);
        Assert.Equal("json", i.UrlParameters["format"]);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_AllowsMethodChaining_WhenSettingProperties()
    {
        Info i = new Info()
            .SetOption("test", "value")
            .SetUrlParameter("output", "rosette");

        Assert.Equal("value", i.Options["test"]);
        Assert.Equal("rosette", i.UrlParameters["output"]);
    }

    #endregion

    #region Instance Creation Tests

    [Fact]
    public void Constructor_CreatesNewInstance_WhenCalled()
    {
        Info i1 = new();
        Info i2 = new();

        Assert.NotSame(i1, i2);
    }

    #endregion

}
