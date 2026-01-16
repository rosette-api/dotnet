namespace rosette_api
{
    public class NameTranslationEndpoint : EndpointCommon<NameTranslationEndpoint>
    {
        private const string NAME = "name";
        private const string ENTITY_TYPE = "entityType";
        private const string SOURCE_LANGUAGE_OF_ORIGIN = "sourceLanguageOfOrigin";
        private const string SOURCE_LANGUAGE_OF_USE = "sourceLanguageOfUse";
        private const string SOURCE_SCRIPT = "sourceScript";
        private const string TARGET_LANGUAGE = "targetLanguage";
        private const string TARGET_SCHEME = "targetScheme";
        private const string TARGET_SCRIPT = "targetScript";

        public NameTranslationEndpoint(string name, string targetLanguage="eng") : base("name-translation") {
            SetName(name);
            SetTargetLanguage(targetLanguage);
        }
        /// <summary>
        /// SetName defines the name to be translated
        /// </summary>
        /// <param name="name">name to be translated</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetName(string name) {
            Params[NAME] = name;

            return this;
        }
        public string Name { get =>
                Params.ContainsKey(NAME) ?
                Params[NAME].ToString() :
                string.Empty;
        }
        /// <summary>
        /// SetEntityType sets the optional entity type, PERSON, LOCATION or ORGANIZATION
        /// </summary>
        /// <param name="entityType">PERSON, LOCATION, or ORGANIZATION</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetEntityType(string entityType) {
            Params[ENTITY_TYPE] = entityType;

            return this;
        }
        public string EntityType { get =>
                Params.ContainsKey(ENTITY_TYPE) ?
                Params[ENTITY_TYPE].ToString() :
                string.Empty;
        }
        /// <summary>
        /// SetSourceLanguageOfOrigin sets the optional ISO 639-3 code for the name's language of origin
        /// </summary>
        /// <param name="sourceLanguageOfOrigin">ISO 639-3 language code</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetSourceLanguageOfOrigin(string sourceLanguageOfOrigin) {
            Params[SOURCE_LANGUAGE_OF_ORIGIN] = sourceLanguageOfOrigin;

            return this;
        }
        public string SourceLanguageOfOrigin { get =>
                Params.ContainsKey(SOURCE_LANGUAGE_OF_ORIGIN) ?
                Params[SOURCE_LANGUAGE_OF_ORIGIN].ToString() :
                string.Empty;
        }
        /// <summary>
        /// SetSourceLanguageOfUse sets the optional ISO 639-3 code for the name's language of use
        /// </summary>
        /// <param name="sourceLanguageOfUse">ISO 639-3 language code</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetSourceLanguageOfUse(string sourceLanguageOfUse) {
            Params[SOURCE_LANGUAGE_OF_USE] = sourceLanguageOfUse;

            return this;
        }
        public string SourceLanguageOfUse { get =>
                Params.ContainsKey(SOURCE_LANGUAGE_OF_USE) ?
                Params[SOURCE_LANGUAGE_OF_USE].ToString() :
                String.Empty;
        }
        /// <summary>
        /// SetSourceScript sets the optional ISO 15924 code for the name's script
        /// </summary>
        /// <param name="sourceScript">ISO 15294 script code</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetSourceScript(string sourceScript) {
            Params[SOURCE_SCRIPT] = sourceScript;

            return this;
        }
        public string SourceScript { get =>
                Params.ContainsKey(SOURCE_SCRIPT) ?
                Params[SOURCE_SCRIPT].ToString() :
                string.Empty;
        }
        /// <summary>
        /// SetTargetLanguage sets the ISO 639-3 code for the translation language.
        /// Defaults to "eng"
        /// </summary>
        /// <param name="targetLanguage">ISO 639-3 language code</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetTargetLanguage(string targetLanguage) {
            Params[TARGET_LANGUAGE] = targetLanguage;

            return this;
        }
        public string TargetLanguage { get =>
                Params.ContainsKey(TARGET_LANGUAGE) ?
                Params[TARGET_LANGUAGE].ToString() :
                String.Empty;
        }
        /// <summary>
        /// SetTargetScheme sets the optional transliteration scheme for the translation
        /// </summary>
        /// <param name="targetScheme">transliteration scheme</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetTargetScheme(string targetScheme) {
            Params[TARGET_SCHEME] = targetScheme;

            return this;
        }
        public string TargetScheme { get =>
                Params.ContainsKey(TARGET_SCHEME) ?
                Params[TARGET_SCHEME].ToString() :
                string.Empty;
        }
        /// <summary>
        /// SetTargetScript sets the optional ISO 15924 code for the translation script
        /// </summary>
        /// <param name="targetScript">ISO 15924 script code</param>
        /// <returns>this</returns>
        public NameTranslationEndpoint SetTargetScript(string targetScript) {
            Params[TARGET_SCRIPT] = targetScript;

            return this;
        }
        public string TargetScript { get => Params.ContainsKey(TARGET_SCRIPT) ?
                Params[TARGET_SCRIPT].ToString() :
                string.Empty;
        }

        public RosetteResponse Call(RosetteAPI api) {
            return Funcs.PostCall(api);
        }
    }
}
