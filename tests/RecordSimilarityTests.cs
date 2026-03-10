using Rosette.Api.Client.Endpoints;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Tests;

public class RecordSimilarityTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsEndpoint_WhenCalledWithValidParameters()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            { "name", new RecordSimilarityFieldInfo("rni_name", 1.0, 0.0) }
        };
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        Assert.Equal("record-similarity", rs.Endpoint);
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenFieldsIsNull()
    {
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        Assert.Throws<ArgumentNullException>(() => new RecordSimilarity(null!, properties, records));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenPropertiesIsNull()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var records = new RecordSimilarityRecords();

        Assert.Throws<ArgumentNullException>(() => new RecordSimilarity(fields, null!, records));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenRecordsIsNull()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();

        Assert.Throws<ArgumentNullException>(() => new RecordSimilarity(fields, properties, null!));
    }

    #endregion

    #region Parameter Tests

    [Fact]
    public void Constructor_AddsFieldsToParams_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            { "name", new RecordSimilarityFieldInfo("rni_name", 1.0, 0.0) }
        };
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        Assert.Contains("fields", rs.Params.Keys);
        Assert.Equal(fields, rs.Params["fields"]);
    }

    [Fact]
    public void Constructor_AddsPropertiesToParams_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        Assert.Contains("properties", rs.Params.Keys);
        Assert.Equal(properties, rs.Params["properties"]);
    }

    [Fact]
    public void Constructor_AddsRecordsToParams_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        Assert.Contains("records", rs.Params.Keys);
        Assert.Equal(records, rs.Params["records"]);
    }

    #endregion

    #region Field Configuration Tests

    [Fact]
    public void Constructor_AcceptsMultipleFields_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            { "name", new RecordSimilarityFieldInfo("rni_name", 1.0, 0.0) },
            { "address", new RecordSimilarityFieldInfo("rni_address", 0.8, 0.0) },
            { "birthDate", new RecordSimilarityFieldInfo("rni_date", 0.6, 0.0) }
        };
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        var storedFields = (Dictionary<string, RecordSimilarityFieldInfo>)rs.Params["fields"];
        Assert.Equal(3, storedFields.Count);
        Assert.Contains("name", storedFields.Keys);
        Assert.Contains("address", storedFields.Keys);
        Assert.Contains("birthDate", storedFields.Keys);
    }

    [Fact]
    public void Constructor_AcceptsFieldInfoWithNullWeight_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            { "name", new RecordSimilarityFieldInfo("rni_name", null, 0.0) }
        };
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        var storedFields = (Dictionary<string, RecordSimilarityFieldInfo>)rs.Params["fields"];
        Assert.Null(storedFields["name"].Weight);
    }

    [Fact]
    public void Constructor_AcceptsFieldInfoWithNullScoreIfNull_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            { "name", new RecordSimilarityFieldInfo("rni_name", 1.0, null) }
        };
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        var storedFields = (Dictionary<string, RecordSimilarityFieldInfo>)rs.Params["fields"];
        Assert.Null(storedFields["name"].ScoreIfNull);
    }

    #endregion

    #region Field Type Tests

    [Theory]
    [InlineData("rni_name")]
    [InlineData("rni_address")]
    [InlineData("rni_date")]
    [InlineData("string")]
    [InlineData("number")]
    [InlineData("boolean")]
    public void Constructor_AcceptsDifferentFieldTypes_WhenCalled(string fieldType)
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>
        {
            { "testField", new RecordSimilarityFieldInfo(fieldType, 1.0, 0.0) }
        };
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        var storedFields = (Dictionary<string, RecordSimilarityFieldInfo>)rs.Params["fields"];
        Assert.Equal(fieldType, storedFields["testField"].Type);
    }

    #endregion

    #region Options Tests

    [Fact]
    public void SetOption_AddsOptionToOptions_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new RecordSimilarity(fields, properties, records)
            .SetOption("debug", true);

        Assert.True((bool)rs.Options["debug"]);
    }

    [Fact]
    public void SetOption_SupportsMultipleOptions_WhenCalledMultipleTimes()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new RecordSimilarity(fields, properties, records)
            .SetOption("debug", true)
            .SetOption("explain", "detailed");

        Assert.True((bool)rs.Options["debug"]);
        Assert.Equal("detailed", rs.Options["explain"]);
    }

    #endregion

    #region Empty Collection Tests

    [Fact]
    public void Constructor_AcceptsEmptyFields_WhenCalled()
    {
        var fields = new Dictionary<string, RecordSimilarityFieldInfo>();
        var properties = new RecordSimilarityProperties();
        var records = new RecordSimilarityRecords();

        RecordSimilarity rs = new(fields, properties, records);

        var storedFields = (Dictionary<string, RecordSimilarityFieldInfo>)rs.Params["fields"];
        Assert.Empty(storedFields);
    }

    #endregion
}
