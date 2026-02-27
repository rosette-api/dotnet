using Rosette.Api.Client.Endpoints.Core;
using Rosette.Api.Client.Models;

namespace Rosette.Api.Client.Endpoints
{
    public class AddressSimilarity : EndpointBase<AddressSimilarity>
    {
        /// <summary>
        /// AddressSimilarityEndpoint checks the similarity between two RosetteAddress objects
        /// </summary>
        /// <param name="address1">RosetteAddress object</param>
        /// <param name="address2">RosetteAddress object</param>
        public AddressSimilarity(AddressField? address1, AddressField? address2) : base("address-similarity")
        {
            ArgumentNullException.ThrowIfNull(address1);
            Params["address1"] = address1;
            ArgumentNullException.ThrowIfNull(address2);
            Params["address2"] = address2;
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