using Rosette.Api.Endpoints.Core;
using Rosette.Api.Models;

namespace Rosette.Api.Endpoints
{
    public class AddressSimilarity : EndpointBase<AddressSimilarity>
    {
        /// <summary>
        /// AddressSimilarityEndpoint checks the similarity between two RosetteAddress objects
        /// </summary>
        /// <param name="address1">RosetteAddress object</param>
        /// <param name="address2">RosetteAddress object</param>
        public AddressSimilarity(IAddress? address1, IAddress? address2) : base("address-similarity")
        {
            ArgumentNullException.ThrowIfNull(address1);
            Params["address1"] = address1;
            ArgumentNullException.ThrowIfNull(address2);
            Params["address2"] = address2;
        }

        public Response Call(ApiClient api)
        {
            return Funcs.PostCall(api);
        }
    }
}