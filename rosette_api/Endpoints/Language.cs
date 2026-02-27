using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints;

public class Language : ContentEndpointBase<Language>
{
    /// <summary>
    /// LanguageEndpoint returns the language extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Language(object content) : base("language", content) { }
}

