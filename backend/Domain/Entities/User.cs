using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnimeFlixBackend.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public UserPreference Preference { get; set; }
        public ICollection<WatchHistory> WatchHistories { get; set; }
        public ICollection<Watchlist> Watchlists { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public ICollection<AIRecommendationLog> AIRecommendationLogs { get; set; }

        public AuthToken Token {  get; set; } 
    }
}

