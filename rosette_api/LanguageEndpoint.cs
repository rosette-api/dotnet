using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api {
    public class LanguageEndpoint : EndpointCommon<LanguageEndpoint> {
        /// <summary>
        /// LanguageEndpoint returns a list of candidate languages in order of descending confidence
        /// </summary>
        /// <param name="content">text, URI or filename</param>
        public LanguageEndpoint(string content) : base("language") {
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, uri or filename</param>
        /// <returns>update Language endpoint</returns>
        public LanguageEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated Language endpoint</returns>
        public LanguageEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated Language endpoint</returns>
        public LanguageEndpoint SetGenre(string genre) {
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
