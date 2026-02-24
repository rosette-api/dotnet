using Rosette.Api.Endpoints.Core;
using Rosette.Api.Models;

namespace Rosette.Api.Endpoints
{
    public class Ping : EndpointBase<Ping>
    {
        public Ping() : base("ping") { }

        public Response Call(ApiClient api) => Funcs.GetCall(api);
    }
}
