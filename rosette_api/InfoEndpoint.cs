using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class InfoEndpoint : EndpointCommon<InfoEndpoint>
    {
        public InfoEndpoint() : base("info") { }

        public RosetteResponse Call(RosetteAPI api) => Funcs.GetCall(api);
    }
}
