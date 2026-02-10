using Rosette.Api.Endpoints.Core;

namespace Rosette.Api.Endpoints
{
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
    public class Morphology : EndpointBase<Morphology> {


        private Dictionary<MorphologyFeature, string> _featureEndpoint;
        /// <summary>
        /// MorphologyEndpoint returns morphological analysis of input
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        /// <param name="feature">feature to use</param>
        public Morphology(object content, MorphologyFeature feature = MorphologyFeature.complete) : base("morphology") {
            Endpoint = "morphology/" + FeatureAsString(feature);
            SetContent(content);
        }
        /// <summary>
        /// SetContent sets the content to be reviewed
        /// </summary>
        /// <param name="content">text, Uri object or FileStream</param>
        /// <returns>update Morphology endpoint</returns>
        public Morphology SetContent(object content) {
            Funcs.Content = content;

            return this;
        }

        public object Content => Funcs.Content;
        /// <summary>
        /// SetLanguage sets the optional ISO 639-3 language code
        /// </summary>
        /// <param name="language">ISO 639-3 language code</param>
        /// <returns>updated Morphology endpoint</returns>
        public Morphology SetLanguage(string language) {
            Funcs.Language = language;

            return this;
        }

        public string? Language => Funcs.Language;
        /// <summary>
        /// SetGenre sets the optional document genre, e.g. social-media
        /// </summary>
        /// <param name="genre">document genre</param>
        /// <returns>updated Morphology endpoint</returns>
        public Morphology SetGenre(string genre) {
            Funcs.Genre = genre;

            return this;
        }

        public string? Genre => Funcs.Genre;
        /// <summary>
        /// SetFileContentType sets the content type of the file contents. Note that
        /// it only applies when the content is a filename
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>updated Morphology endpoint</returns>
        public Morphology SetFileContentType(string contentType) {
            Funcs.FileContentType = contentType;

            return this;
        }
        public string FileContentType => Funcs.FileContentType;
        public string Filename => Funcs.Filename;
        /// <summary>
        /// Call passes the data to the server and returns the response
        /// </summary>
        /// <param name="api">RosetteAPI object</param>
        /// <returns>RosetteResponse</returns>
        public Response Call(ApiClient api) {
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
