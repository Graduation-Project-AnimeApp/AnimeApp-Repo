namespace AnimeFlixBackend.Application.DTOs
{
    public class WatchHistoryDto
    {
        public int HistoryId { get; set; }
        public int AnimeApiId { get; set; }
        public DateTime WatchedAt { get; set; }
    }

}
