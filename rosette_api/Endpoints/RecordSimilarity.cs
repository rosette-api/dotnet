using Rosette.Api.Endpoints.Core;
using Rosette.Api.Models;

namespace Rosette.Api.Endpoints;

public class RecordSimilarity : EndpointBase<RecordSimilarity>
{
    /// <summary>
    /// RecordSimilarity returns the similarity score between records
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public RecordSimilarity(Dictionary<string, RecordSimilarityFieldInfo>? fields, 
        RecordSimilarityProperties? properties, 
        RecordSimilarityRecords? records) : base("record-similarity")
    {
        ArgumentNullException.ThrowIfNull(fields);
        Params["fields"] = fields;
        ArgumentNullException.ThrowIfNull(properties);
        Params["properties"] = properties;
        ArgumentNullException.ThrowIfNull(records);
        Params["records"] = records;
    }

    public new Task<Response> CallAsync(ApiClient api, CancellationToken cancellationToken = default)
    {
        return Funcs.PostCallAsync(api, cancellationToken);
    }

    public new Response Call(ApiClient api)
    {
        return Funcs.PostCall(api);
    }
}