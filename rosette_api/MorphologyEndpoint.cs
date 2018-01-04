﻿using System;
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
        /// <summary>
        /// MorphologyEndpoint returns morphological analysis of input
        /// </summary>
        /// <param name="content">text, URI or filename</param>
        /// <param name="feature">feature to use</param>
        public MorphologyEndpoint(string content, MorphologyFeature feature = MorphologyFeature.complete) : base("morphology") {
            Endpoint = "morphology/" + FeatureAsString(feature);
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, uri or filename</param>
        /// <returns>update Morphology endpoint</returns>
        public MorphologyEndpoint SetContent(string content) {
            Funcs.Content = content;

            return this;
        }

        public string Content { get => Funcs.Content; }
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated Morphology endpoint</returns>
        public MorphologyEndpoint SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string Language { get => Funcs.Language; }
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated Morphology endpoint</returns>
        public MorphologyEndpoint SetGenre(string genre) {
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
        /// <summary>
        /// FeatureAsString returns the feature enum in its endpoint string form
        /// </summary>
        /// <param name="feature">feature</param>
        /// <returns>endpoint equivalent</returns>
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
