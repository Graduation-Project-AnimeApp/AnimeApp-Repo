using AnimeFlixBackend.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IAnimeService
    {
        Task<AnimeDto?> GetAnimeAsync(int malId);

        Task<List<AnimeDto>> SearchAnimeAsync(string? query, int page);

        Task<List<AnimeDto>> GetLatestAnimeAsync();
    }
}