using Rosette.Api.Endpoints;
using Rosette.Api.Models;

namespace Rosette.Api.Tests;

public class NameSimilarityTests
{
    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenNamesAreNull() {
        var exception = Record.Exception(() => new NameSimilarity(null, null));
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal("Value cannot be null. (Parameter 'name1')", exception.Message);

        Name rn = new Name("foo");
        exception = Record.Exception(() => new NameSimilarity(rn, null));
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal("Value cannot be null. (Parameter 'name2')", exception.Message);
    }
}
