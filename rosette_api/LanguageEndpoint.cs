namespace rosette_api;

public class LanguageEndpoint : ContentBasedEndpoint<LanguageEndpoint>
{
    /// <summary>
    /// LanguageEndpoint returns the language extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public LanguageEndpoint(object content) : base("language", content)
    {
    }
}
