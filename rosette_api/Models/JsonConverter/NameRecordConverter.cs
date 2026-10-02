namespace Rosette.Api.Client.Models.JsonConverter;

/// <summary>Converts a name record to and from a single name or an array of names.</summary>
public class NameRecordConverter : StructuredFieldConverter<NameRecord, NameFieldData>
{
    protected override List<NameFieldData> GetData(NameRecord field) => field.Data;
    protected override NameRecord Create(List<NameFieldData> data) => new(data);
}


