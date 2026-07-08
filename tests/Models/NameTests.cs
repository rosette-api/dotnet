using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests.Models;

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

    [Fact]
    public void Constructor_ThrowsArgumentException_WhenTextIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new Name(null!));
    }

    [Fact]
    public void Constructor_ThrowsArgumentException_WhenTextIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Name(string.Empty));
    }

    [Fact]
    public void Constructor_ThrowsArgumentException_WhenTextIsWhitespace()
    {
        Assert.Throws<ArgumentException>(() => new Name("   "));
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
    public void SetEntityType_WithEnum_SetsEntityType_WhenCalled()
    {
        var name = new Name("foo").SetEntityType(Client.Models.EntityType.Person);
        Assert.Equal("PERSON", name.EntityType);
    }

    [Fact]
    public void SetEntityType_WithEnum_SetsLocation_WhenCalled()
    {
        var name = new Name("foo").SetEntityType(Client.Models.EntityType.Location);
        Assert.Equal("LOCATION", name.EntityType);
    }

    [Fact]
    public void SetEntityType_WithEnum_SetsOrganization_WhenCalled()
    {
        var name = new Name("foo").SetEntityType(Client.Models.EntityType.Organization);
        Assert.Equal("ORGANIZATION", name.EntityType);
    }

    [Fact]
    public void SetEntityType_ThrowsArgumentException_WhenTypeIsNull()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentNullException>(() => name.SetEntityType(null!));
    }

    [Fact]
    public void SetEntityType_ThrowsArgumentException_WhenTypeIsEmpty()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentException>(() => name.SetEntityType(string.Empty));
    }

    [Fact]
    public void SetEntityType_ThrowsArgumentException_WhenTypeIsWhitespace()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentException>(() => name.SetEntityType("   "));
    }

    [Fact]
    public void SetEntityType_ThrowsArgumentException_WhenTypeIsInvalid()
    {
        var name = new Name("foo");
        var exception = Assert.Throws<ArgumentException>(() => name.SetEntityType("INVALID_TYPE"));
        Assert.Contains("Entity type must be one of", exception.Message);
    }

    [Fact]
    public void SetEntityType_AcceptsLocation_WhenCalled()
    {
        var name = new Name("foo").SetEntityType("LOCATION");
        Assert.Equal("LOCATION", name.EntityType);
    }

    [Fact]
    public void SetEntityType_AcceptsOrganization_WhenCalled()
    {
        var name = new Name("foo").SetEntityType("ORGANIZATION");
        Assert.Equal("ORGANIZATION", name.EntityType);
    }

    [Fact]
    public void SetEntityType_IsCaseInsensitive_WhenCalled()
    {
        var name = new Name("foo").SetEntityType("person");
        Assert.Equal("PERSON", name.EntityType);
    }

    [Fact]
    public void SetLanguage_SetsLanguage_WhenCalled() {
        Name rn = new Name("foo").SetLanguage("eng");
        Assert.Equal("foo", rn.Text);
        Assert.Equal("eng", rn.Language);
    }

    [Fact]
    public void SetLanguage_ThrowsArgumentException_WhenLanguageIsNull()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentNullException>(() => name.SetLanguage(null!));
    }

    [Fact]
    public void SetLanguage_ThrowsArgumentException_WhenLanguageIsEmpty()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentException>(() => name.SetLanguage(string.Empty));
    }

    [Fact]
    public void SetLanguage_ThrowsArgumentException_WhenLanguageIsWhitespace()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentException>(() => name.SetLanguage("   "));
    }

    [Fact]
    public void SetScript_SetsScript_WhenCalled() {
        Name rn = new Name("foo").SetScript("zho");
        Assert.Equal("foo", rn.Text);
        Assert.Equal("zho", rn.Script);
    }

    [Fact]
    public void SetScript_ThrowsArgumentException_WhenScriptIsNull()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentNullException>(() => name.SetScript(null!));
    }

    [Fact]
    public void SetScript_ThrowsArgumentException_WhenScriptIsEmpty()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentException>(() => name.SetScript(string.Empty));
    }

    [Fact]
    public void SetScript_ThrowsArgumentException_WhenScriptIsWhitespace()
    {
        var name = new Name("foo");
        Assert.Throws<ArgumentException>(() => name.SetScript("   "));
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
