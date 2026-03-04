using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints;

public class Sentiment : ContentEndpointBase<Sentiment>
{
    /// <summary>
    /// SentimentEndpoint analyzes the positive and negative sentiment expressed by the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Sentiment(object content) : base("sentiment", content) { }
}
