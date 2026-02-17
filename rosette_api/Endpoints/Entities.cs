using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints;

public class Entities : ContentEndpointBase<Entities>
{
    /// <summary>
    /// EntitiesEndpoint returns the entities extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public Entities(object content) : base("entities", content)
    {
    }

    /// <summary>
    /// SetGenre is not supported for the Entities endpoint and will be ignored.
    /// This override prevents the genre from being set.
    /// </summary>
    /// <param name="genre">document genre (ignored)</param>
    /// <returns>Updated endpoint instance</returns>
    public new Entities SetGenre(string genre)
    {
        // Genre is not supported for Entities endpoint - ignore the value
        return this;
    }
}
