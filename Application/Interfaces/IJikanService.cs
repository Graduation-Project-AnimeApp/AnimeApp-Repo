using AnimeFlixBackend.Infrastructure.External;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IJikanService
    {
        Task<JikanAnime?> GetAnimeByMalIdAsync(int malId);
        Task<List<JikanAnime>> SearchAnimeAsync(string? query, int page);
        Task<List<JikanAnime>> GetLatestAnimeAsync();
        Task<List<JikanReview>> GetAnimeReviewsAsync(int malId);
    }
}