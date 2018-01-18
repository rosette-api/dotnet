using Xunit;

namespace rosette_api.tests
{
    public class TestRosetteName
    {
        [Fact]
        public void CheckName() {
            RosetteName rn = new RosetteName("foo");
            Assert.Equal("foo", rn.Text);
            Assert.Null(rn.EntityType);
            Assert.Null(rn.Language);
            Assert.Null(rn.Script);
        }

        [Fact]
        public void CheckWithEntityType() {
            RosetteName rn = new RosetteName("foo").SetEntityType("PERSON");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("PERSON", rn.EntityType);
        }

        [Fact]
        public void CheckWithLanguage() {
            RosetteName rn = new RosetteName("foo").SetLanguage("eng");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("eng", rn.Language);
        }

        [Fact]
        public void CheckWithScript() {
            RosetteName rn = new RosetteName("foo").SetScript("zho");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("zho", rn.Script);
        }

        [Fact]
        public void CheckAll() {
            RosetteName rn = new RosetteName("foo")
                .SetEntityType("PERSON")
                .SetLanguage("eng")
                .SetScript("zho");
            Assert.Equal("foo", rn.Text);
            Assert.Equal("PERSON", rn.EntityType);
            Assert.Equal("eng", rn.Language);
            Assert.Equal("zho", rn.Script);
        }
    }
}
