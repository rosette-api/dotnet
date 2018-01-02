using System;
using System.Collections.Generic;
using System.Text;

namespace rosette_api
{
    public class TransliterationEndpoint : EndpointCommon<TransliterationEndpoint> {
        public TransliterationEndpoint() : base("transliteration") {
        }

        public TransliterationEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }

        public TransliterationEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }

        public TransliterationEndpoint SetGenre(string genre) {
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
