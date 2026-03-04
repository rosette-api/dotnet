using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints;

public class Tokens : ContentEndpointBase<Tokens> {
    /// <summary>
    /// TokensEndpoint returns each entity extracted from the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Tokens(object content) : base("tokens", content) { }
}
