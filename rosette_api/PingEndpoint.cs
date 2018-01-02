using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class PingEndpoint : EndpointCommon<PingEndpoint>
    {
        public PingEndpoint() : base("ping") { }

        public RosetteResponse Call(RosetteAPI api) => Funcs.GetCall(api);
    }
}
