using System;

namespace AnimeFlix.Models
{
    public class WatchHistory
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public int AnimeApiId { get; set; }
        public string WatchStatus { get; set; } // Watching / Completed
        public DateTime WatchedAt { get; set; } = DateTime.Now;

        public User User { get; set; }
    }
}
