using Rosette.Api.Endpoints.Core;
using Rosette.Api.Models;

namespace Rosette.Api.Endpoints
{
    public class NameSimilarity : EndpointBase<NameSimilarity> {
        /// <summary>
        /// NameSimilarityEndpoint checks the similarity between two RosetteName objects
        /// </summary>
        /// <param name="name1">RosetteName object</param>
        /// <param name="name2">RosetteName object</param>
        public NameSimilarity(Name? name1, Name? name2) : base("name-similarity") {
            ArgumentNullException.ThrowIfNull(name1);
            Params["name1"] = name1;
            ArgumentNullException.ThrowIfNull(name2);
            Params["name2"] = name2;
        }

        public new Task<Response> CallAsync(ApiClient api, CancellationToken cancellationToken = default)
        {
            return Funcs.PostCallAsync(api, cancellationToken);
        }

        public new Response Call(ApiClient api)
        {
            return Funcs.PostCall(api);
        }
    }
}
