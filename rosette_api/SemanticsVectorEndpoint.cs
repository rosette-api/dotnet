namespace rosette_api;

public class SemanticsVectorEndpoint : ContentBasedEndpoint<SemanticsVectorEndpoint>
{
    /// <summary>
    /// SemanticVectorsEndpoint returns the relationships between entities in the input text
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public SemanticsVectorEndpoint(object content) : base("semantics/vector", content)
    {
    }
}