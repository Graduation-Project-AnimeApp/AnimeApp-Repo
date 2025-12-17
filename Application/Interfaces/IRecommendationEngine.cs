using AnimeFlixBackend.Application.DTOs;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IRecommendationEngine
    {        Task<List<RecommendationDto>> GenerateNewRecommendationsAsync(int userId);
    }
}
