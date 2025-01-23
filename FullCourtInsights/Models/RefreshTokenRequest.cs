using System.Text.Json.Serialization;

namespace FullCourtInsights.Models
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
