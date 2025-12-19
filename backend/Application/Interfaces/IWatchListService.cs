using AnimeFlixBackend.Application.DTOs;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IWatchlistService
    {
        Task AddAsync(int userId, int animeApiId);
        Task<List<WatchlistDto>> GetUserWatchlist(int userId);
    }


}
