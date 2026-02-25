using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class SemanticsVector : ContentEndpointBase<SemanticsVector>
{
    /// <summary>
    /// SemanticVectorsEndpoint returns the relationships between entities in the input text
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public SemanticsVector(object content) : base("semantics/vector", content) { }
}
