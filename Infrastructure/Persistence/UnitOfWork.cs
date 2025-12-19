// Using the complete version, removing the redundant one.
using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using AnimeFlixBackend.Infrastructure.Repositories; // Assuming GenericRepository is here

namespace AnimeFlixBackend.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // IUnitOfWork properties for all repositories
        public IGenericRepository<Watchlist> Watchlists { get; }
        public IGenericRepository<WatchHistory> WatchHistories { get; }
        public IGenericRepository<Review> Reviews { get; }
        public IGenericRepository<AIRecommendationLog> RecommendationLogs { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            // Initialize all repositories
            Watchlists = new GenericRepository<Watchlist>(context);
            WatchHistories = new GenericRepository<WatchHistory>(context);
            Reviews = new GenericRepository<Review>(context);
            RecommendationLogs = new GenericRepository<AIRecommendationLog>(context);
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync(); // Correctly commits changes from all repositories
    }
}