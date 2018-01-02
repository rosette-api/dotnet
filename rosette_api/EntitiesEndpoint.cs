using System;
using System.Collections.Generic;

namespace rosette_api
{
    public class EntitiesEndpoint : EndpointCommon<EntitiesEndpoint>
    {
        public EntitiesEndpoint() : base("entities") {
        }

        public EntitiesEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }

        public EntitiesEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }

        public EntitiesEndpoint SetGenre(string genre) {
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
