using Rosette.Api;
using Rosette.Api.Models;

namespace examples
{
    class RecordSimilarity
    {
        /// <summary>
        /// RunEndpoint runs the example.  By default the endpoint will be run against the Rosette Cloud Service.
        /// An optional alternate URL may be provided, i.e. for an on-premise solution.
        /// </summary>
        /// <param name="apiKey">Required api key (obtained from Basis Technology)</param>
        /// <param name="altUrl">Optional alternate URL</param>
        private void RunEndpoint(string apiKey, string? altUrl = null)
        {
            try
            {
                ApiClient api = new ApiClient(apiKey);
                if (!string.IsNullOrEmpty(altUrl))
                {
                    api.UseAlternateURL(altUrl);
                }

                // record field names
                string primaryNameField = "primaryName";
                string dobField = "dob";
                string dob2Field = "dob2";
                string addrField = "addr";
                string jobField = "jobTitle";
                string ageField = "age";
                string retiredField = "isRetired";
                string dobHyphen = "1993-04-16";

                // Creating the request object
                Dictionary<string, RecordSimilarityFieldInfo> fields = new Dictionary<string, RecordSimilarityFieldInfo>
                {
                    { primaryNameField, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniName, Weight = 0.5 } },
                    { dobField, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniDate, Weight = 0.2 } },
                    { dob2Field, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniDate, Weight = 0.1 } },
                    { addrField, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniAddress, Weight = 0.5 } },
                    { jobField, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniString, Weight = 0.2 } },
                    { ageField, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniNumber, Weight = 0.4 } },
                    { retiredField, new RecordSimilarityFieldInfo { Type = RecordFieldType.RniBoolean, Weight = 0.05 } }
                };

                RecordSimilarityProperties properties = new RecordSimilarityProperties { Threshold = 0.7, IncludeExplainInfo = true };

                RecordSimilarityRecords records = new RecordSimilarityRecords
                {
                    Left = new List<Dictionary<string, RecordSimilarityField>>
                    {
                        new Dictionary<string, RecordSimilarityField>
                        {
                            { primaryNameField, new FieldedNameRecord { Text = "Ethan R", Language = "eng", LanguageOfOrigin = "eng", Script = "Latn", EntityType = "PERSON"} },
                            { dobField, new UnfieldedDateRecord { Date = dobHyphen} },
                            { dob2Field, new FieldedDateRecord { Date = "04161993", Format = "MMddyyyy"} },
                            { addrField, new UnfieldedAddressRecord { Address = "123 Roadlane Ave"} },
                            { jobField, new StringRecord { Text = "software engineer"} }
                        },
                        new Dictionary<string, RecordSimilarityField>
                        {
                            { primaryNameField, new FieldedNameRecord { Text = "Evan R"} },
                            { dobField, new FieldedDateRecord { Date = dobHyphen} },
                            { ageField, new NumberRecord { Number = 47.344 } },
                            { retiredField, new BooleanRecord { Boolean = false } }
                        }
                    },
                    Right = new List<Dictionary<string, RecordSimilarityField>>
                    {
                        new Dictionary<string, RecordSimilarityField>
                        {
                            { primaryNameField, new FieldedNameRecord { Text = "Seth R", Language = "eng"} },
                            { dobField, new FieldedDateRecord { Date = dobHyphen} },
                            { jobField, new StringRecord { Text = "manager"} },
                            { retiredField, new BooleanRecord { Boolean = true } }
                        },
                        new Dictionary<string, RecordSimilarityField>
                        {
                            { primaryNameField, new UnfieldedNameRecord { Text = "Ivan R"} },
                            { dobField, new FieldedDateRecord { Date = dobHyphen} },
                            { dob2Field, new FieldedDateRecord { Date = "1993/04/16"} },
                            { addrField, new FieldedAddressRecord { HouseNumber = "123", Road = "Roadlane Ave"} },
                            { ageField, new NumberRecord { Number = 72 } },
                            { retiredField, new BooleanRecord { Boolean = true } }
                        }
                    }
                };

                Rosette.Api.Endpoints.RecordSimilarity endpoint = new(fields, properties, records);
                Response response = endpoint.Call(api);

                // Print out the response headers
                foreach (KeyValuePair<string, string> h in response.Headers)
                {
                    Console.WriteLine(string.Format("{0}:{1}", h.Key, h.Value));
                }
                // Print out the content in JSON format.  The Content property returns an IDictionary.
                Console.WriteLine(response.ContentAsJson(pretty: true));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        /// <summary>
        /// Main is a simple entrypoint for command line calling of the endpoint examples
        /// </summary>
        /// <param name="args">Command line args, expects API Key, (optional) alt URL</param>
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                new RecordSimilarity().RunEndpoint(args[0], args.Length > 1 ? args[1] : null);
            }
            else
            {
                Console.WriteLine("An API Key is required");
            }
        }
    }
}