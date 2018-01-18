using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class TextEmbeddingEndpoint : EndpointCommon<TextEmbeddingEndpoint> {
        /// <summary>
        /// TextEmbeddingEndpoint returns the embedding for the input text
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        public TextEmbeddingEndpoint(object content) : base("text-embedding") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        /// <returns>update TextEmbedding endpoint</returns>
        public TextEmbeddingEndpoint SetContent(object content) {
            Funcs.Content = content;

            return this;
        }

        public object Content { get => Funcs.Content; }
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated TextEmbedding endpoint</returns>
        public TextEmbeddingEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated TextEmbedding endpoint</returns>
        public TextEmbeddingEndpoint SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string Genre { get => Funcs.Genre; }
        /// <summary>
        /// SetFileContentType sets the content type of the file contents. Note that
        /// it only applies when the content is a filename
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>updated TextEmbedding endpoint</returns>
        public TextEmbeddingEndpoint SetFileContentType(string contentType) {
            Funcs.FileContentType = contentType;

            return this;
        }
        public string FileContentType { get => Funcs.FileContentType; }
        public string Filename { get => Funcs.Filename; }
        /// <summary>
        /// Call passes the data to the server and returns the response
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>RosetteResponse</returns>
        public RosetteResponse Call(RosetteAPI api) {
            return Funcs.PostCall(api);
        }
    }
}
