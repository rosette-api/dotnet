namespace rosette_api
{
    public class SentimentEndpoint : ContentBasedEndpoint<SentimentEndpoint>
    {
        /// <summary>
        /// SentimentEndpoint analyzes the positive and negative sentiment expressed by the input
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        public SentimentEndpoint(object content) : base("sentiment", content)
        {
        }
    }
}