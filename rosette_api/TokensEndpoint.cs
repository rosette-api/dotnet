namespace rosette_api;

public class TokensEndpoint : ContentBasedEndpoint<TokensEndpoint> {
    /// <summary>
    /// TokensEndpoint returns each entity extracted from the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public TokensEndpoint(object content) : base("tokens", content) {
    }
}
