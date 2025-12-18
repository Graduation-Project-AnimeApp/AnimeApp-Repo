namespace AnimeFlixBackend.Domain.Entities
{
    public class UserPreference
    {
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public string FavoriteGenres { get; set; } // Comma-separated genres
        public string PreferredMood { get; set; }

        public User User { get; set; }
    }
}
