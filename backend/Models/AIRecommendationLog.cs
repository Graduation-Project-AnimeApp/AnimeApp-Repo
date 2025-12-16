using AnimeFlix.Models;

public class AIRecommendationLog
{
    public int LogId { get; set; }
    public int UserId { get; set; }
    public int AnimeApiId { get; set; }
    public string Reason { get; set; }   // e.g. "Based on Action + Excited mood"
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User User { get; set; }
}
