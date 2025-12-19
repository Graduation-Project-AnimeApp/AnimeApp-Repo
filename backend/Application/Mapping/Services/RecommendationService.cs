using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using System.Linq;

namespace AnimeFlixBackend.Application.Mapping.Services
{
    // This class correctly implements the updated IRecommendationService interface
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRecommendationEngine _engine;
        private readonly IAnimeService _animeService;

        public RecommendationService(IUnitOfWork uow, IRecommendationEngine engine, IAnimeService animeService)
        {
            _uow = uow;
            _engine = engine;
            _animeService = animeService;
        }

        public async Task<List<AnimeDto>> GetRecommendations(int userId)
        {
            // 1. Check existing logs
            var logs = await _uow.RecommendationLogs.FindAsync(x => x.UserId == userId);

            // Simple check: If no logs, generate new ones.
            if (!logs.Any())
            {
                // 2. Generate new recommendations using the engine
                var newRecommendations = await _engine.GenerateNewRecommendationsAsync(userId);

                // If the engine returns nothing, we exit early with an empty list.
                if (!newRecommendations.Any())
                {
                    return new List<AnimeDto>();
                }

                // 3. Log the new recommendations to the database
                foreach (var rec in newRecommendations)
                {
                    await _uow.RecommendationLogs.AddAsync(new AIRecommendationLog
                    {
                        UserId = userId,
                        AnimeApiId = rec.AnimeApiId,
                        Reason = rec.Reason,
                        CreatedAt = DateTime.Now
                    });
                }
                await _uow.SaveChangesAsync();

                // Create a temporary log list from the generated DTOs for enrichment.
                logs = newRecommendations
                    .Select(rec => new AIRecommendationLog
                    {
                        AnimeApiId = rec.AnimeApiId,
                        Reason = rec.Reason
                    })
                    .ToList();
            }

            // 4. Enrich the DTOs: Fetch full Anime details for the recommended IDs
            var recommendedAnimeDtos = new List<AnimeDto>();

            // DistinctBy requires System.Linq
            foreach (var log in logs.DistinctBy(l => l.AnimeApiId))
            {
                // Call the existing AnimeService to get the full Jikan/enriched DTO
                var animeDto = await _animeService.GetAnimeAsync(log.AnimeApiId);

                if (animeDto != null)
                {
                    // Attach the AI specific details (Reason)
                    animeDto.RecommendationReason = log.Reason;
                    recommendedAnimeDtos.Add(animeDto);
                }
            }

            return recommendedAnimeDtos;
        }

    }
}