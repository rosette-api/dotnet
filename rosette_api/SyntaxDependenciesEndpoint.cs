using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class SyntaxDependenciesEndpoint : EndpointCommon<SyntaxDependenciesEndpoint> {
        public SyntaxDependenciesEndpoint() : base("syntax/dependencies") {
        }

        public SyntaxDependenciesEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }

        public SyntaxDependenciesEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }

        public SyntaxDependenciesEndpoint SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string Genre { get => Funcs.Genre; }

        public string Filename { get => Funcs.Filename; }

        public RosetteResponse Call(RosetteAPI api) {
            return Funcs.PostCall(api);
        }
    }
}
