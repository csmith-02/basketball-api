using System.Text.Json.Serialization;

namespace FullCourtInsights.Models
{
    public class PlayerRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstname")]
        public required string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public required string LastName { get; set; }

        [JsonPropertyName("birth")]
        public Dictionary<string, string>? Birth { get; set; }

        [JsonPropertyName("nba")]
        public Dictionary<string, int>? Nba { get; set; }

        [JsonPropertyName("height")]
        public Dictionary<string, string?>? Height { get; set; }

        [JsonPropertyName("weight")]
        public Dictionary<string, string?>? Weight { get; set; }

        [JsonPropertyName("college")]
        public string? College { get; set; }

        [JsonPropertyName("affiliation")]
        public string? Affiliation { get; set; }

        [JsonPropertyName("leagues")]
        public Dictionary<string, object>? Leagues { get; set; }

    }
}
