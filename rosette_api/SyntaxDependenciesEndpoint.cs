namespace rosette_api;

public class SyntaxDependenciesEndpoint : ContentBasedEndpoint<SyntaxDependenciesEndpoint> {
    /// <summary>
    /// SyntaxDependenciesEndpoint returns the parse tree of the input text as a list of labeled directed links between tokens, as well as the list of tokens in the input sentence
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public SyntaxDependenciesEndpoint(object content) : base("syntax/dependencies", content) {
    }
}
