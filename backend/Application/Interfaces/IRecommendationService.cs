using AnimeFlixBackend.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<List<AnimeDto>> GetRecommendations(int userId);
    }
}