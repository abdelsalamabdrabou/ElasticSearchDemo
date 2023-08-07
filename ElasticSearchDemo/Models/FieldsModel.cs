using System.Text.Json.Serialization;

namespace ElasticSearchDemo.Models
{
    public sealed class FieldsModel
    {
        [JsonPropertyName("SourceContext")]
        public string SourceContext { get; set; }

        [JsonPropertyName("ActionName")]
        public string ActionName { get; set; }

        [JsonPropertyName("RequestPath")]
        public string RequestPath { get; set; }

        [JsonPropertyName("MachineName")]
        public string MachineName { get; set; }

        [JsonPropertyName("ApplicationName")]
        public string ApplicationName { get; set; }
    }
}
