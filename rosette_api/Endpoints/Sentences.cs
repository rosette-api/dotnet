using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Sentences : ContentEndpointBase<Sentences> {
    /// <summary>
    /// SentencesEndpoint returns each entity extracted from the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Sentences(object content) : base("sentences", content) { }
}
