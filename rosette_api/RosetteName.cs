using System.Text.Json.Serialization;

namespace rosette_api
{
    public class RosetteName
    {
        [JsonPropertyName("text")]
        public string Text { get; private set; }
        [JsonPropertyName("entityType")]
        public string? EntityType { get; private set; }
        [JsonPropertyName("language")]
        public string? Language { get; private set; }
        [JsonPropertyName("script")]
        public string? Script { get; private set; }
        /// <summary>
        /// Constructor for a Name object, used by several endpoints
        /// </summary>
        /// <param name="text">required text</param>
        public RosetteName(string text) {
            Text = text;
        }
        /// <summary>
        /// SetEntityType sets the optional entity type.  PERSON, LOCATION and ORGANIZATION
        /// are currently supported.
        /// </summary>
        /// <param name="type">entity type, PERSON, LOCATION or ORGANIZATION</param>
        /// <returns>updated RosetteName object</returns>
        public RosetteName SetEntityType(string type) {
            EntityType = type;
            return this;
        }
        /// <summary>
        /// SetLanguage sets the optional ISO-639-3 language code for the name's language
        /// </summary>
        /// <param name="language">ISO-639-3 language code</param>
        /// <returns>updated RosetteName object</returns>
        public RosetteName SetLanguage(string language) {
            Language = language;
            return this;
        }
        /// <summary>
        /// SetScript sets the ISO-15924 code for the name's script
        /// </summary>
        /// <param name="script">ISO-15924 script code</param>
        /// <returns>updated RosetteName object</returns>
        public RosetteName SetScript(string script) {
            Script = script;
            return this;
        }


    }
}
