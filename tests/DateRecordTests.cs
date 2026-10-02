using System.Text.Json;
using Rosette.Api.Client.Models;
using static Rosette.Api.Tests.RecordSimilarityTestData;

namespace Rosette.Api.Tests;

public class DateRecordTests
{
    [Theory]
    [InlineData("{\"date\":\"1993-04-16\"}")]
    [InlineData("{\"date\":\"1993-04-16\",\"format\":null}")]
    [InlineData("[{\"date\":\"1993-04-16\"}]")]
    public void FieldedDate_WithoutFormat_RoundTripsThroughWrapper(string json)
    {
        var wrapper = JsonSerializer.Deserialize<DateRecord>(json)!;
        var date = Assert.IsType<FieldedDateRecord>(Assert.Single(wrapper.Data));
        Assert.Equal("1993-04-16", date.Date);
        Assert.Null(date.Format);
        Assert.Equal(wrapper, JsonSerializer.Deserialize<DateRecord>(JsonSerializer.Serialize(wrapper)));
        Assert.Equal("{\"date\":\"1993-04-16\"}", Write(wrapper));
    }
}
