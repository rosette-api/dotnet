namespace rosette_api;

public class TopicsEndpoint : ContentBasedEndpoint<TopicsEndpoint> {
    /// <summary>
    /// TopicsEndpoint returns the topic extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public TopicsEndpoint(object content) : base("topics", content) {
    }
}
