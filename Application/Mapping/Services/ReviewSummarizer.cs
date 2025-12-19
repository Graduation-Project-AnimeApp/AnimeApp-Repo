using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using Google.GenAI;
using Microsoft.EntityFrameworkCore;

namespace AnimeFlixBackend.Application.Services
{
    public class ReviewSummarizerService : IReviewSummarizerService
    {
        private readonly Client _geminiClient;
        private readonly AppDbContext _context;
        private readonly IJikanService _jikan;

        public ReviewSummarizerService(Client geminiClient, AppDbContext context, IJikanService jikan)
        {
            _geminiClient = geminiClient;
            _context = context;
            _jikan = jikan; 
        }

        public async Task SummarizeReviewsAsync(int malId)
        {
            // 1. Get real reviews from Jikan
            var reviews = await _jikan.GetAnimeReviewsAsync(malId);
            if (reviews == null || !reviews.Any()) return;

            // Combine text from top 5 reviews
            var reviewContent = string.Join(" ", reviews.Take(5).Select(r => r.Content));

            // 2. Ask Gemini to summarize
            var prompt = $"Summarize these anime reviews into one concise paragraph: {reviewContent}";
            var response = await _geminiClient.Models.GenerateContentAsync("gemini-2.5-flash", prompt);
            var summary = response.Candidates[0].Content.Parts[0].Text;

            // 3. SAVE to database for User 1 (The AI System)
            var existing = await _context.Reviews
                .FirstOrDefaultAsync(r => r.AnimeApiId == malId && r.UserId == 1);

            if (existing != null)
            {
                existing.ReviewText = summary;
                existing.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                _context.Reviews.Add(new Review
                {
                    UserId = 1,
                    AnimeApiId = malId,
                    ReviewText = summary,
                    CreatedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}