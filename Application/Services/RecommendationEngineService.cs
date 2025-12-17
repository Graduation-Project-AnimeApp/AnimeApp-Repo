using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;

namespace AnimeFlixBackend.Application.Services
{
    // Inside Application/Services (for demonstration)
    public class SimpleRecommendationEngine : IRecommendationEngine
    {
        private readonly IUnitOfWork _uow;

        // Inject Unit of Work to access user history and preferences
        public SimpleRecommendationEngine(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<RecommendationDto>> GenerateNewRecommendationsAsync(int userId)
        {
            // 1. Get user history/preferences (Example: Load data)
            var history = await _uow.WatchHistories.FindAsync(h => h.UserId == userId);
            // UserPreference logic would be used here to fetch favorite genres, moods, etc.
            // var preferences = await _uow.UserPreferences.GetByUserIdAsync(userId); 

            // 2. Mock Logic: Recommend a few specific anime if the user has watched anything.
            // In a real AI model, this would be a complex calculation.
            if (history.Count > 0)
            {
                return new List<RecommendationDto>
            {
                new RecommendationDto { AnimeApiId = 9253, Reason = "Recommended based on your love for action and sci-fi." }, // Steins;Gate
                new RecommendationDto { AnimeApiId = 30276, Reason = "Popular choice among users with similar viewing history." } // One Punch Man
            };
            }

            // Default list if no history exists
            return new List<RecommendationDto>
        {
            new RecommendationDto { AnimeApiId = 1, Reason = "Top rated anime globally." } // Cowboy Bebop
        };
        }
    }
}
