namespace rosette_api;

public class EntitiesEndpoint : ContentBasedEndpoint<EntitiesEndpoint>
{
    public EntitiesEndpoint(object content) : base("entities", content)
    {
    }
}
