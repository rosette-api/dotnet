namespace rosette_api
{
    public class NameSimilarityEndpoint : EndpointCommon<NameSimilarityEndpoint> {
        /// <summary>
        /// NameSimilarityEndpoint checks the similarity between two RosetteName objects
        /// </summary>
        /// <param name="name1">RosetteName object</param>
        /// <param name="name2">RosetteName object</param>
        public NameSimilarityEndpoint(RosetteName? name1, RosetteName? name2) : base("name-similarity") {
            Params["name1"] = name1 ?? throw new ArgumentNullException("name1", "Name1 cannot be null");
            Params["name2"] = name2 ?? throw new ArgumentNullException("name2", "Name2 cannot be null");
        }

        public RosetteResponse Call(RosetteAPI api) {
            return Funcs.PostCall(api);
        }
    }
}
