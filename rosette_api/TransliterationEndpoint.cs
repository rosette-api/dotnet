namespace rosette_api;

public class TransliterationEndpoint : ContentBasedEndpoint<TransliterationEndpoint> {
    /// <summary>
    /// TransliterationEndpoint returns the transliteration of the input
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public TransliterationEndpoint(object content) : base("transliteration", content) {
    }
}
