namespace Rosette.Api.Client.Models.JsonConverter;

public class AddressFieldConverter : StructuredFieldConverter<AddressRecord, AddressFieldData>
{
    protected override List<AddressFieldData> GetData(AddressRecord field) => field.Data;
    protected override AddressRecord Create(List<AddressFieldData> data) => new(data);
}
