using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class CategoriesEndpoint : EndpointCommon<CategoriesEndpoint> {
        /// <summary>
        /// CategoriesEndpoint returns a category identified in the input. The possible categories are Tier 1 contextual categories defined in the QAG Taxonomy
        /// </summary>
        /// <param name="content">text, URI or filename</param>
        public CategoriesEndpoint(string content) : base("categories") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, uri or filename</param>
        /// <returns>update Categories endpoint</returns>
        public CategoriesEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated Categories endpoint</returns>
        public CategoriesEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated Categories endpoint</returns>
        public CategoriesEndpoint SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string Genre { get => Funcs.Genre; }
        /// <summary>
        /// SetFileContentType sets the content type of the file contents. Note that
        /// it only applies when the content is a filename
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>updated Categories endpoint</returns>
        public CategoriesEndpoint SetFileContentType(string contentType) {
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
