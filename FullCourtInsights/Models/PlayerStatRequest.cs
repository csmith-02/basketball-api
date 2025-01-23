using System.Text.Json.Serialization;

namespace FullCourtInsights.Models
{
    public class PlayerStatRequest
    {

        [JsonPropertyName("player")]
        public Player? Player { get; set; }

        [JsonPropertyName("team")]
        public Team? Team { get; set; }

        [JsonPropertyName("game")]
        public Game? Game { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }

        [JsonPropertyName("pos")]
        public string? Position { get; set; }

        [JsonPropertyName("min")]
        public string? Minutes { get; set; }

        [JsonPropertyName("fgm")]
        public int FieldGoalMade { get; set; }

        [JsonPropertyName("fga")]
        public int FieldGoalAttempted { get; set; }

        [JsonPropertyName("fgp")]
        public string? FieldGoalPercentage { get; set; }

        [JsonPropertyName("ftm")]
        public int FreeThrowsMade { get; set; }

        [JsonPropertyName("fta")]
        public int FreeThrowsAttempted { get; set; }

        [JsonPropertyName("ftp")]
        public string? FreeThrowPercentage { get; set; }

        [JsonPropertyName("tpm")]
        public int ThreePointersMade { get; set; }

        [JsonPropertyName("tpa")]
        public int ThreePointersAttempted { get; set; }

        [JsonPropertyName("tpp")]
        public string? ThreePointPercentage { get; set; }

        [JsonPropertyName("offReb")]
        public int OffensiveRebounds { get; set; }

        [JsonPropertyName("defReb")]
        public int DefensiveRebounds { get; set; }

        [JsonPropertyName("totReb")]
        public int TotalRebounds { get; set; }

        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("pFouls")]
        public int PersonalFouls { get; set; }

        [JsonPropertyName("steals")]
        public int Steals { get; set; }

        [JsonPropertyName("turnovers")]
        public int Turnovers { get; set; }

        [JsonPropertyName("blocks")]
        public int Blocks { get; set; }

    }
}
