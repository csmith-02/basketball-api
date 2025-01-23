namespace FullCourtInsights.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }

        public DateTime Exp {  get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
