using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints;

public class Events : ContentEndpointBase<Events>
{
    /// <summary>
    /// EventsEndpoint returns the events extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Events(object content) : base("events", content) { }
}
