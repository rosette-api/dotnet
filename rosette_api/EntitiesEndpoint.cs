namespace rosette_api;

public class EntitiesEndpoint : ContentBasedEndpoint<EntitiesEndpoint>
{
    /// <summary>
    /// EntitiesEndpoint returns the entities extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public EntitiesEndpoint(object content) : base("entities", content)
    {
    }
}
