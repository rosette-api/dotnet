namespace rosette_api;

public class CategoriesEndpoint : ContentBasedEndpoint<CategoriesEndpoint>
{
    /// <summary>
    /// CategoriesEndpoint returns the categories extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public CategoriesEndpoint(object content) : base("categories", content)
    {
    }
}
