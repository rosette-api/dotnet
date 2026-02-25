using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Sentiment : ContentEndpointBase<Sentiment>
{
    /// <summary>
    /// SentimentEndpoint analyzes the positive and negative sentiment expressed by the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Sentiment(object content) : base("sentiment", content) { }
}
