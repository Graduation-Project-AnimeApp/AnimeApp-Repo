using System;
using System.Collections.Generic;

namespace AnimeFlix.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public UserPreference Preference { get; set; }
        public ICollection<WatchHistory> WatchHistories { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
