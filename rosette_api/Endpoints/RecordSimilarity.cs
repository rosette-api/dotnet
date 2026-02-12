using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class RecordSimilarity : ContentEndpointBase<RecordSimilarity>
{
    /// <summary>
    /// RecordSimilarity returns the similarity score between records
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public RecordSimilarity(object content) : base("record-similarity", content)
    {
    }
}