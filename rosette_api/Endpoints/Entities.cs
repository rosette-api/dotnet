using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Entities : ContentEndpointBase<Entities>
{
    /// <summary>
    /// EntitiesEndpoint returns the entities extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Entities(object content) : base("entities", content) { }
}

