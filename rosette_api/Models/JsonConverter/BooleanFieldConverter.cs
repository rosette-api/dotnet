namespace Rosette.Api.Client.Models.JsonConverter;

public class BooleanFieldConverter : CollectionFieldConverter<BooleanRecord, bool>
{
    protected override List<bool> GetData(BooleanRecord field) => field.Data;
    protected override BooleanRecord Create(List<bool> data) => new(data);
}
