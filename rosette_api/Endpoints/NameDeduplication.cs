using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints
{
    public class NameDeduplication : EndpointBase<NameDeduplication>
    {
        /// <summary>
        /// NameDeduplicationEndpoint constructs an object to deduplicate names through the Rosette API
        /// </summary>
        /// <param name="names">List of RosetteName objects</param>
        public NameDeduplication(List<Name> names) : base("name-deduplication") {
            SetNames(names);
        }
        /// <summary>
        /// SetNames assigns a list of RosetteName objects to be processed
        /// </summary>
        /// <param name="names">List of RosetteName</param>
        /// <returns>updated NameDeduplicationEndpoint object</returns>
        public NameDeduplication SetNames(List<Name> names) {
            if (names == null || names.Count == 0) {
                throw new ArgumentException("Names must contain at least 1 RosetteName object.");
            }
            Params["names"] = names;
            return this;
        }
        /// <summary>
        /// Names returns the list of RosetteName objects
        /// </summary>
        public IList<Name> Names { get =>
                Params.ContainsKey("names") ?
                Params["names"] as IList<Name> :
                null;
        }
        /// <summary>
        /// SetProfileID sets a profile ID to be used during processing
        /// </summary>
        /// <param name="profileID">profile ID</param>
        /// <returns>updated NameDeduplicationEndpoint object</returns>
        public NameDeduplication SetProfileID(string profileID) {
            Params["profileid"] = profileID;
            return this;
        }
        /// <summary>
        /// ProfileID returns the profile ID or empty string
        /// </summary>
        public string ProfileID { get =>
                Params.ContainsKey("profileid") ?
                Params["profileid"].ToString() :
                string.Empty;
        }
        /// <summary>
        /// SetThreshold sets the threshold to be used for determining deduplication cluster sizing
        /// </summary>
        /// <param name="threshold">float value of threshold, default 0.75</param>
        /// <returns>updated NameDeduplicationEndpoint object</returns>
        public NameDeduplication SetThreshold(float threshold) {
            Params["threshold"] = threshold;
            return this;
        }
        /// <summary>
        /// Threshold returns the threshold or the default, 0.75f
        /// </summary>
        public float Threshold { get =>
                Params.ContainsKey("threshold") ?
                (float)Params["threshold"] :
                0.75f;
        }
        /// <summary>
        /// Call returns the response from the server
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>RosetteResponse</returns>
        public Response Call(ApiClient api) {
            return Funcs.PostCall(api);
        }

    }
}
