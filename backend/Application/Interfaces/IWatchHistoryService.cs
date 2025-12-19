using AnimeFlixBackend.Application.DTOs;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IWatchHistoryService
    {
        Task AddAsync(int userId, int animeApiId);
        Task<List<WatchHistoryDto>> GetUserHistory(int userId);
    }

}
