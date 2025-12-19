using AnimeFlixBackend.Domain.Entities;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Watchlist> Watchlists { get; }
        IGenericRepository<WatchHistory> WatchHistories { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<AIRecommendationLog> RecommendationLogs { get; }

        Task<int> SaveChangesAsync();
    }


}
