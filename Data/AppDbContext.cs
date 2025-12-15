using Microsoft.EntityFrameworkCore;
using AnimeFlix.Models;

namespace AnimeFlix.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<WatchHistory>().HasKey(h => h.HistoryId);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<UserPreference>().HasKey(p => p.PreferenceId);
            // Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // One-to-One: User ↔ UserPreference
            modelBuilder.Entity<UserPreference>()
                .HasOne(p => p.User)
                .WithOne(u => u.Preference)
                .HasForeignKey<UserPreference>(p => p.UserId);

            // One-to-Many: User ↔ WatchHistory
            modelBuilder.Entity<WatchHistory>()
                .HasOne(h => h.User)
                .WithMany(u => u.WatchHistories)
                .HasForeignKey(h => h.UserId);

            // One-to-Many: User ↔ Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "Esraa",
                    Email = "esraa@example.com",
                    PasswordHash = "e3afed0047b08059d0fada10f400c1e5", 
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = 2,
                    Username = "Ali",
                    Email = "ali@example.com",
                    PasswordHash = "e3afed0047b08059d0fada10f400c1e5",
                    CreatedAt = DateTime.Now
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
                    AnimeApiId = 32, // Example anime ID from API
                    WatchStatus = "Watching",
                    WatchedAt = DateTime.Now.AddDays(-1)
                },
                new WatchHistory
                {
                    HistoryId = 2,
                    UserId = 2,
                    AnimeApiId = 30,
                    WatchStatus = "Completed",
                    WatchedAt = DateTime.Now.AddDays(-5)
                }
            );
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    ReviewId = 1,
                    UserId = 1,
                    AnimeApiId = 32,
                    ReviewText = "Amazing anime! Great action scenes.",
                    CreatedAt = DateTime.Now
                },
                new Review
                {
                    ReviewId = 2,
                    UserId = 2,
                    AnimeApiId = 30,
                    ReviewText = "Very emotional story. Loved it!",
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
