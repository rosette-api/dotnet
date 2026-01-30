namespace rosette_api;

public class RelationshipsEndpoint : ContentBasedEndpoint<RelationshipsEndpoint>
{
    /// <summary>
    /// RelationshipsEndpoint returns the relationships between entities in the input text
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public RelationshipsEndpoint(object content) : base("relationships", content)
    {
    }
}