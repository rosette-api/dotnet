using Rosette.Api.Models;

namespace Rosette.Api.Tests
{
    public class NameTests
    {
        [Fact]
        public void CheckName() {
            Name rn = new Name("foo");
            Assert.Equal("foo", rn.Text);
            Assert.Null(rn.EntityType);
            Assert.Null(rn.Language);
            Assert.Null(rn.Script);
            Assert.Null(rn.Gender);
        }

        [Fact]
        public void CheckWithEntityType() {
            Name rn = new Name("foo").SetEntityType("PERSON");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("PERSON", rn.EntityType);
        }

        [Fact]
        public void CheckWithLanguage() {
            Name rn = new Name("foo").SetLanguage("eng");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("eng", rn.Language);
        }

        [Fact]
        public void CheckWithScript() {
            Name rn = new Name("foo").SetScript("zho");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("zho", rn.Script);
        }

        [Fact]
        public void CheckWithGender()
        {
            Name rn = new Name("foo").SetGender(GenderType.Female);
            Assert.Equal("foo", rn.Text);
            Assert.Equal(GenderType.Female, rn.Gender);
        }

        [Fact]
        public void CheckAll() {
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
    }
}
