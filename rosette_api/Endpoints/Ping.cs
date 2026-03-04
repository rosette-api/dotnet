using Rosette.Api.Client.Endpoints.Core;

namespace Rosette.Api.Client.Endpoints
{
    public class Ping : EndpointBase<Ping>
    {
        public Ping() : base("ping") { }
    }
}
