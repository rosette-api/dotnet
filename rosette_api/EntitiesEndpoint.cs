using System;
using System.Collections.Generic;

namespace rosette_api
{
    public class EntitiesEndpoint : EndpointCommon<EntitiesEndpoint> {
        /// <summary>
        /// EntitiesEndpoint returns each entity extracted from the input
        /// </summary>
        /// <param name="content">text, URI or filename</param>
        public EntitiesEndpoint(string content) : base("entities") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, uri or filename</param>
        /// <returns>update Entities endpoint</returns>
        public EntitiesEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated Entities endpoint</returns>
        public EntitiesEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated Entities endpoint</returns>
        public EntitiesEndpoint SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string Genre { get => Funcs.Genre; }
        /// <summary>
        /// SetFileContentType sets the content type of the file contents. Note that
        /// it only applies when the content is a filename
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>updated Entities endpoint</returns>
        public EntitiesEndpoint SetFileContentType(string contentType) {
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
