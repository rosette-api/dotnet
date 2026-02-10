namespace rosette_api;

public class EventsEndpoint : ContentBasedEndpoint<EventsEndpoint>
{
    /// <summary>
    /// EventsEndpoint returns the events extracted from the endpoint
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    public EventsEndpoint(object content) : base("events", content)
    {
    }
}