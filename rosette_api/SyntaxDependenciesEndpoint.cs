using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class SyntaxDependenciesEndpoint : EndpointCommon<SyntaxDependenciesEndpoint> {
        /// <summary>
        /// SyntaxDependenciesEndpoint returns the parse tree of the input text as a list of labeled directed links between tokens, as well as the list of tokens in the input sentence
        /// </summary>
        /// <param name="content">text, URI or filename</param>
        public SyntaxDependenciesEndpoint(string content) : base("syntax/dependencies") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, uri or filename</param>
        /// <returns>update SyntaxDependencies endpoint</returns>
        public SyntaxDependenciesEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }
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
