using Rosette.Api.Endpoints.Core;
using Rosette.Api.Models;

namespace Rosette.Api.Endpoints
{
    public class Info : EndpointBase<Info>
    {
        public Info() : base("info") { }

        public Response Call(ApiClient api) => Funcs.GetCall(api);
    }
}
