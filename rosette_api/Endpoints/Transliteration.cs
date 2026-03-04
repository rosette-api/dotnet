using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints;

public class Transliteration : ContentEndpointBase<Transliteration> {
    /// <summary>
    /// TransliterationEndpoint returns the transliteration of the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Transliteration(object content) : base("transliteration", content) { }
}
