namespace rosette_api
{
    public class SentencesEndpoint : ContentBasedEndpoint<SentencesEndpoint> {
        /// <summary>
        /// SentencesEndpoint returns each entity extracted from the input
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        public SentencesEndpoint(object content) : base("sentences", content) {
        }
    }
}
