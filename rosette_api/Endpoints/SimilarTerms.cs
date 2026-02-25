using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class SimilarTerms : ContentEndpointBase<SimilarTerms>
{
    /// <summary>
    /// SimilarTerms returns terms that are similar to the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public SimilarTerms(object content) : base("semantics/similar", content) { }
}
