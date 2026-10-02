namespace Rosette.Api.Client.Models.JsonConverter;

public class DateFieldConverter : StructuredFieldConverter<DateRecord, DateFieldData>
{
    protected override List<DateFieldData> GetData(DateRecord field) => field.Data;
    protected override DateRecord Create(List<DateFieldData> data) => new(data);
}
