using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class SyntaxDependenciesEndpoint : EndpointCommon<SyntaxDependenciesEndpoint> {
        /// <summary>
        /// SyntaxDependenciesEndpoint returns the parse tree of the input text as a list of labeled directed links between tokens, as well as the list of tokens in the input sentence
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        public SyntaxDependenciesEndpoint(object content) : base("syntax/dependencies") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        /// <returns>update SyntaxDependencies endpoint</returns>
        public SyntaxDependenciesEndpoint SetContent(object content) {
            Funcs.Content = content;

            return this;
        }

        public object Content { get => Funcs.Content; }
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated SyntaxDependencies endpoint</returns>
        public SyntaxDependenciesEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated SyntaxDependencies endpoint</returns>
        public SyntaxDependenciesEndpoint SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string Genre { get => Funcs.Genre; }
        /// <summary>
        /// SetFileContentType sets the content type of the file contents. Note that
        /// it only applies when the content is a filename
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>updated SyntaxDependencies endpoint</returns>
        public SyntaxDependenciesEndpoint SetFileContentType(string contentType) {
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
