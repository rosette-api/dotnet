using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Relationships : ContentEndpointBase<Relationships>
{
    /// <summary>
    /// RelationshipsEndpoint returns the relationships between entities in the input text
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Relationships(object content) : base("relationships", content) { }
}
