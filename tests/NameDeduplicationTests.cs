using Rosette.Api.Endpoints;
using Rosette.Api.Models;

namespace Rosette.Api.Tests;

public class NameDeduplicationTests
{
    [Fact]
    public void Constructor_SetsNamesAndThreshold_WhenCalledWithNames() {
        List<Name> names = new List<Name> {
            new Name("foo"),
            new Name("bar")
        };
        NameDeduplication n = new NameDeduplication(names);
        Assert.Equal(names, n.Names);
        Assert.Equal(0.75f, n.Threshold);
    }

    [Fact]
    public void SetProfileID_SetsProfileID_WhenCalled() {
        List<Name> names = new List<Name> {
            new Name("foo"),
            new Name("bar")
        };
        NameDeduplication n = new NameDeduplication(names).SetProfileID("profileid");
        Assert.Equal(names, n.Names);
        Assert.Equal("profileid", n.ProfileID);
    }

    [Fact]
    public void SetThreshold_SetsThreshold_WhenCalled() {
        List<Name> names = new List<Name> {
            new Name("foo"),
            new Name("bar")
        };
        NameDeduplication n = new NameDeduplication(names).SetThreshold(0.8f);
        Assert.Equal(names, n.Names);
        Assert.Equal(0.8f, n.Threshold);
    }
}
