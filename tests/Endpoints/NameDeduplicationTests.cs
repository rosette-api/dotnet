using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests.Endpoints;

public class NameDeduplicationTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsNamesAndThreshold_WhenCalledWithNames() {
        List<Name> names = new()
        {
            new Name("foo"),
            new Name("bar")
        };
        NameDeduplication n = new(names);
        Assert.Equal(names, n.Names);
        Assert.Equal(0.75f, n.Threshold);
    }

    #endregion

    #region Property Configuration Tests

    [Fact]
    public void SetProfileID_SetsProfileID_WhenCalled() {
        List<Name> names = new()
        {
            new Name("foo"),
            new Name("bar")
        };
        NameDeduplication n = new NameDeduplication(names).SetProfileID("profileid");
        Assert.Equal(names, n.Names);
        Assert.Equal("profileid", n.ProfileID);
    }

    [Fact]
    public void SetThreshold_SetsThreshold_WhenCalled() {
        List<Name> names = new()
        {
            new Name("foo"),
            new Name("bar")
        };
        NameDeduplication n = new NameDeduplication(names).SetThreshold(0.8f);
        Assert.Equal(names, n.Names);
        Assert.Equal(0.8f, n.Threshold);
    }

    #endregion
}
