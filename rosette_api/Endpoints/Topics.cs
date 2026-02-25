using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Topics : ContentEndpointBase<Topics> {
    /// <summary>
    /// TopicsEndpoint returns the topic extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Topics(object content) : base("topics", content) { }
}
