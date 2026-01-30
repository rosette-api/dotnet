namespace rosette_api;

/// <summary>
/// Abstract base class for Rosette API endpoints that accept content and common parameters
/// </summary>
/// <typeparam name="T">The concrete endpoint type for fluent API support</typeparam>
public abstract class ContentBasedEndpoint<T> : EndpointCommon<T> where T : ContentBasedEndpoint<T>
{
    /// <summary>
    /// Constructor for content-based endpoints
    /// </summary>
    /// <param name="endpointName">The API endpoint name (e.g., "language", "categories", "entities")</param>
    /// <param name="content">text, Uri object or FileStream</param>
    protected ContentBasedEndpoint(string endpointName, object content) : base(endpointName) {
        ArgumentNullException.ThrowIfNull(content);
        SetContent(content);
    }

    /// <summary>
    /// SetContent sets the content to be processed
    /// </summary>
    /// <param name="content">text, Uri object or FileStream</param>
    /// <returns>Updated endpoint instance</returns>
    public T SetContent(object content) {
        ArgumentNullException.ThrowIfNull(content);
        Funcs.Content = content;
        return (T)this;
    }

    /// <summary>
    /// Gets the content to be processed
    /// </summary>
    public object Content => Funcs.Content;

    /// <summary>
    /// SetLanguage sets the optional ISO 639-3 language code
    /// </summary>
    /// <param name="language">ISO 639-3 language code</param>
    /// <returns>Updated endpoint instance</returns>
    public T SetLanguage(string language) {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        Funcs.Language = language;
        return (T)this;
    }

    /// <summary>
    /// Gets the ISO 639-3 language code
    /// </summary>
    public string? Language => Funcs.Language;

    /// <summary>
    /// SetGenre sets the optional document genre, e.g. social-media
    /// </summary>
    /// <param name="genre">document genre</param>
    /// <returns>Updated endpoint instance</returns>
    public T SetGenre(string genre) {
        ArgumentException.ThrowIfNullOrWhiteSpace(genre);
        Funcs.Genre = genre;
        return (T)this;
    }

    /// <summary>
    /// Gets the document genre
    /// </summary>
    public string? Genre => Funcs.Genre;

    /// <summary>
    /// SetFileContentType sets the content type of the file contents. Note that
    /// it only applies when the content is a filename
    /// </summary>
    /// <param name="contentType">Content-Type</param>
    /// <returns>Updated endpoint instance</returns>
    public T SetFileContentType(string contentType) {
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);
        Funcs.FileContentType = contentType;
        return (T)this;
    }

    /// <summary>
    /// Gets the file content type for multipart uploads
    /// </summary>
    public string FileContentType => Funcs.FileContentType;

    /// <summary>
    /// Gets the filename if content is a FileStream
    /// </summary>
    public string Filename => Funcs.Filename;

    /// <summary>
    /// Call passes the data to the server and returns the response
    /// </summary>
    /// <param name="api">RosetteAPI object</param>
    /// <returns>RosetteResponse</returns>
    public RosetteResponse Call(RosetteAPI api) {
        return Funcs.PostCall(api);
    }
}