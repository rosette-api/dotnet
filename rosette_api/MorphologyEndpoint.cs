using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace rosette_api
{
    public class MorphologyEndpoint : EndpointCommon<MorphologyEndpoint> {

        public enum MorphologyFeature {
            /// <summary>provide complete morphology</summary>
            complete,
            /// <summary>provide lemmas</summary>
            lemmas,
            /// <summary>provide parts of speech</summary>
            partsOfSpeech,
            /// <summary>provide compound components</summary>
            compoundComponents,
            /// <summary>provide han readings</summary>
            hanReadings
        };

        private Dictionary<MorphologyFeature, string> _featureEndpoint;
        public MorphologyEndpoint(MorphologyFeature feature) : base("morphology") {
            Endpoint = "morphology/" + FeatureAsString(feature);
        }

        public MorphologyEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }

        public MorphologyEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }

        public MorphologyEndpoint SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string Genre { get => Funcs.Genre; }

        public string Filename { get => Funcs.Filename; }

        public RosetteResponse Call(RosetteAPI api) {
            return Funcs.PostCall(api);
        }

        public string FeatureAsString(MorphologyFeature feature) {
            _featureEndpoint = new Dictionary<MorphologyFeature, string>() {
                { MorphologyFeature.complete, "complete" },
                { MorphologyFeature.lemmas, "lemmas" },
                { MorphologyFeature.partsOfSpeech, "parts-of-speech" },
                { MorphologyFeature.compoundComponents, "compound-components" },
                { MorphologyFeature.hanReadings, "han-readings" }
            };

            return _featureEndpoint[feature];
        }

    }
}
