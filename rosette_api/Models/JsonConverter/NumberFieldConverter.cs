namespace Rosette.Api.Client.Models.JsonConverter;

public class NumberFieldConverter : CollectionFieldConverter<NumberRecord, double>
{
    protected override List<double> GetData(NumberRecord field) => field.Data;
    protected override NumberRecord Create(List<double> data) => new(data);
}
