using Rosette.Api.Client.Endpoints.Core;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Client.Endpoints;

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
        SetComparisonMethod(RecordSimilarityComparisonMethod.ONE_TO_ONE);
    }

    /// <summary>
    /// RecordSimilarity returns the similarity score between records
    /// </summary>
    /// <param name="fields">The record field definitions</param>
    /// <param name="properties">The record similarity properties</param>
    /// <param name="records">The records to compare</param>
    /// <param name="comparisonMethod">The record comparison method</param>
    public RecordSimilarity(Dictionary<string, RecordSimilarityFieldInfo>? fields,
        RecordSimilarityProperties? properties,
        RecordSimilarityRecords? records,
        RecordSimilarityComparisonMethod comparisonMethod) : this(fields, properties, records)
    {
        SetComparisonMethod(comparisonMethod);
    }

    /// <summary>
    /// SetComparisonMethod sets the record comparison method
    /// </summary>
    /// <param name="comparisonMethod">The record comparison method</param>
    /// <returns>this</returns>
    public RecordSimilarity SetComparisonMethod(RecordSimilarityComparisonMethod comparisonMethod)
    {
        if (!Enum.IsDefined(comparisonMethod))
        {
            throw new ArgumentOutOfRangeException(nameof(comparisonMethod), comparisonMethod, "Unknown comparison method.");
        }

        Params["comparisonMethod"] = comparisonMethod;
        return this;
    }

    /// <summary>
    /// Gets the record comparison method
    /// </summary>
    public RecordSimilarityComparisonMethod ComparisonMethod =>
        (RecordSimilarityComparisonMethod)Params["comparisonMethod"];

    public new Task<Response> CallAsync(ApiClient api, CancellationToken cancellationToken = default)
    {
        return Funcs.PostCallAsync(api, cancellationToken);
    }

    public new Response Call(ApiClient api)
    {
        return Funcs.PostCall(api);
    }
}