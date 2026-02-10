using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Tokens : ContentEndpointBase<Tokens> {
    /// <summary>
    /// TokensEndpoint returns each entity extracted from the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Tokens(object content) : base("tokens", content) {
    }
}
