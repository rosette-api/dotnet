using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints;

public class TextEmbedding : ContentEndpointBase<TextEmbedding>
{
    /// <summary>
    /// TextEmbeddingEndpoint returns the embedding for the input text
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public TextEmbedding(object content) : base("text-embedding", content) { }
}