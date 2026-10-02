namespace Rosette.Api.Client.Models.JsonConverter;

public class StringFieldConverter : CollectionFieldConverter<StringRecord, string>
{
    protected override List<string> GetData(StringRecord field) => field.Data;
    protected override StringRecord Create(List<string> data) => new(data);
}
