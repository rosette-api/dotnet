using System.Net.Http;
using System.Threading.Tasks;

namespace rosette_api
{
    public class BaseGetEndpoint<T> where T : BaseGetEndpoint<T>
    {
        /// <summary>
        /// Endpoint returns the specified endpoint
        /// </summary>
        public string Endpoint { get; protected set; }
        /// <summary>
        /// BaseGetEndpoint is the base endpoint for Get operations
        /// </summary>
        public BaseGetEndpoint() {}
        /// <summary>
        /// Call executes the endpoint against the server
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>RosetteResponse</returns>
        public RosetteResponse Call(RosetteAPI api) {
            string url = api.URI + Endpoint;
            Task<HttpResponseMessage> task = Task.Run<HttpResponseMessage>(async () => await api.Client.GetAsync(url));
            var response = task.Result;

            return new RosetteResponse(response);
        }
    }
}
