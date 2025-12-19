namespace AnimeFlixBackend.Application.DTOs
{
    public class WatchlistDto
    {
        public int WatchListId { get; set; }
        public int AnimeApiId { get; set; }
        public DateTime AddedAt { get; set; }
    }

}
