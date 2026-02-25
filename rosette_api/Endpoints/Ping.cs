using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints
{
    public class Ping : EndpointBase<Ping>
    {
        public Ping() : base("ping") { }
    }
}
