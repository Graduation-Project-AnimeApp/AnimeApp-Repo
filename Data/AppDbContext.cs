using Microsoft.EntityFrameworkCore;
using AnimeFlixBackend.Domain.Entities;

namespace AnimeFlix.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AIRecommendationLog> AIRecommendationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<UserPreference>().HasKey(p => p.PreferenceId);
            modelBuilder.Entity<WatchHistory>().HasKey(h => h.HistoryId);
            modelBuilder.Entity<Watchlist>().HasKey(w => w.WatchListId);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<AIRecommendationLog>().HasKey(a => a.LogId);

           
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

  

            // User ↔ UserPreference (One-to-One)
            modelBuilder.Entity<UserPreference>()
                .HasOne(p => p.User)
                .WithOne(u => u.Preference)
                .HasForeignKey<UserPreference>(p => p.UserId);

            // User ↔ WatchHistory (One-to-Many)
            modelBuilder.Entity<WatchHistory>()
                .HasOne(h => h.User)
                .WithMany(u => u.WatchHistories)
                .HasForeignKey(h => h.UserId);

            // User ↔ Watchlist (One-to-Many)
            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watchlists)
                .HasForeignKey(w => w.UserId);

            // User ↔ Review (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            // User ↔ AIRecommendationLog (One-to-Many)
            modelBuilder.Entity<AIRecommendationLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AIRecommendationLogs)
                .HasForeignKey(a => a.UserId);

            // Seed Data (Fixed Dates)
            var seedDate = new DateTime(2025, 1, 1);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "Esraa",
                    Email = "esraa@example.com",
                    PasswordHash = "e3afed0047b08059d0fada10f400c1e5",
                    CreatedAt = seedDate
                },
                new User
                {
                    UserId = 2,
                    Username = "Ali",
                    Email = "ali@example.com",
                    PasswordHash = "e3afed0047b08059d0fada10f400c1e5",
                    CreatedAt = seedDate
                }
            );

            modelBuilder.Entity<UserPreference>().HasData(
                new UserPreference
                {
                    PreferenceId = 1,
                    UserId = 1,
                    FavoriteGenres = "Action,Adventure",
                    PreferredMood = "Excited"
                },
                new UserPreference
                {
                    PreferenceId = 2,
                    UserId = 2,
                    FavoriteGenres = "Romance,Drama",
                    PreferredMood = "Relaxed"
                }
            );

            modelBuilder.Entity<WatchHistory>().HasData(
                new WatchHistory
                {
                    HistoryId = 1,
                    UserId = 1,
                    AnimeApiId = 32,
                    WatchedAt = seedDate
                }
            );

            modelBuilder.Entity<Watchlist>().HasData(
                new Watchlist
                {
                    WatchListId = 1,
                    UserId = 2,
                    AnimeApiId = 45,
                    AddedAt = seedDate
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    ReviewId = 1,
                    UserId = 1,
                    AnimeApiId = 32,
                    ReviewText = "Amazing anime!",
                    CreatedAt = seedDate
                }
            );

            modelBuilder.Entity<AIRecommendationLog>().HasData(
                new AIRecommendationLog
                {
                    LogId = 1,
                    UserId = 1,
                    AnimeApiId = 99,
                    Reason = "Based on Action genre and Excited mood",
                    CreatedAt = seedDate
                }
            );
        }
    }
}
