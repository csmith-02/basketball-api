using System.Text.Json.Serialization;

namespace FullCourtInsights.Models
{
    public class StatApiResponse
    {
        [JsonPropertyName("get")]
        public string? Get { get; set; }

        [JsonPropertyName("parameters")]
        public Dictionary<string, string>? Parameters { get; set; }

        [JsonPropertyName("errors")]
        public string[] Errors { get; set; } = [];

        [JsonPropertyName("results")]
        public int Results { get; set; }

        [JsonPropertyName("response")]
        public IEnumerable<PlayerStatRequest>? Response { get; set; }
    }
}
