namespace AnimeFlixBackend.Domain.Entities
{
    public class Watchlist
    {
        public int WatchListId { get; set; }
        public int UserId { get; set; }
        public int AnimeApiId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public User User { get; set; }

    }
}
