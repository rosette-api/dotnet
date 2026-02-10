using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Transliteration : ContentEndpointBase<Transliteration> {
    /// <summary>
    /// TransliterationEndpoint returns the transliteration of the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Transliteration(object content) : base("transliteration", content) {
    }
}
