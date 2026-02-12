using Rosette.Api.Endpoints.Core;
using Rosette.Api.Models;

namespace Rosette.Api.Endpoints
{
    public class TextEmbedding : EndpointBase<TextEmbedding> {
        /// <summary>
        /// TextEmbeddingEndpoint returns the embedding for the input text
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        public TextEmbedding(object content) : base("text-embedding") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        /// <returns>update TextEmbedding endpoint</returns>
        public TextEmbedding SetContent(object content) {
            Funcs.Content = content;

            return this;
        }

        public object Content => Funcs.Content;
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated TextEmbedding endpoint</returns>
        public TextEmbedding SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string? Language => Funcs.Language;
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated TextEmbedding endpoint</returns>
        public TextEmbedding SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string? Genre => Funcs.Genre;
        /// <summary>
        /// SetFileContentType sets the content type of the file contents. Note that
        /// it only applies when the content is a filename
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>updated TextEmbedding endpoint</returns>
        public TextEmbedding SetFileContentType(string contentType) {
            Funcs.FileContentType = contentType;

            return this;
        }
        public string FileContentType => Funcs.FileContentType;
        public string Filename => Funcs.Filename;
        /// <summary>
        /// Call passes the data to the server and returns the response
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>RosetteResponse</returns>
        public Response Call(ApiClient api) {
            return Funcs.PostCall(api);
        }
    }
}
