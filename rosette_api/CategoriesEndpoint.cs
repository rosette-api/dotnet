namespace rosette_api;

public class CategoriesEndpoint : ContentBasedEndpoint<CategoriesEndpoint>
{
    public CategoriesEndpoint(object content) : base("categories", content)
    {
    }
}
