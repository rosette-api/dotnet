using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class NameTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsTextAndNullProperties_WhenCreatingName() {
        Name rn = new("foo");
        Assert.Equal("foo", rn.Text);
        Assert.Null(rn.EntityType);
        Assert.Null(rn.Language);
        Assert.Null(rn.Script);
        Assert.Null(rn.Gender);
    }

    #endregion

    #region Property Setting Tests

    [Fact]
    public void SetEntityType_SetsEntityType_WhenCalled() {
        Name rn = new Name("foo").SetEntityType("PERSON");
        Assert.Equal("foo", rn.Text);
        Assert.Equal("PERSON", rn.EntityType);
    }

    [Fact]
    public void SetLanguage_SetsLanguage_WhenCalled() {
        Name rn = new Name("foo").SetLanguage("eng");
        Assert.Equal("foo", rn.Text);
        Assert.Equal("eng", rn.Language);
    }

    [Fact]
    public void SetScript_SetsScript_WhenCalled() {
        Name rn = new Name("foo").SetScript("zho");
        Assert.Equal("foo", rn.Text);
        Assert.Equal("zho", rn.Script);
    }

    [Fact]
    public void SetGender_SetsGender_WhenCalled()
    {
        Name rn = new Name("foo").SetGender(GenderType.Female);
        Assert.Equal("foo", rn.Text);
        Assert.Equal(GenderType.Female, rn.Gender);
    }

    #endregion

    #region Fluent API Tests

    [Fact]
    public void FluentAPI_SetsAllProperties_WhenChaining() {
        Name rn = new Name("foo")
            .SetEntityType("PERSON")
            .SetLanguage("eng")
            .SetScript("zho")
            .SetGender(GenderType.Male);
        Assert.Equal("foo", rn.Text);
        Assert.Equal("PERSON", rn.EntityType);
        Assert.Equal("eng", rn.Language);
        Assert.Equal("zho", rn.Script);
        Assert.Equal(GenderType.Male, rn.Gender);
    }

    #endregion
}
