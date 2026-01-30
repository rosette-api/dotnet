namespace rosette_api
{
    public class NameSimilarityEndpoint : EndpointCommon<NameSimilarityEndpoint> {
        /// <summary>
        /// NameSimilarityEndpoint checks the similarity between two RosetteName objects
        /// </summary>
        /// <param name="name1">RosetteName object</param>
        /// <param name="name2">RosetteName object</param>
        public NameSimilarityEndpoint(RosetteName? name1, RosetteName? name2) : base("name-similarity") {
            ArgumentNullException.ThrowIfNull(name1);
            Params["name1"] = name1;
            ArgumentNullException.ThrowIfNull(name2);
            Params["name2"] = name2;
        }

        public RosetteResponse Call(RosetteAPI api) {
            return Funcs.PostCall(api);
        }
    }
}
