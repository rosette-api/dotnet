using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Categories : ContentEndpointBase<Categories>
{
    /// <summary>
    /// CategoriesEndpoint returns the categories extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Categories(object content) : base("categories", content)
    {
    }
}
