using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using Google.GenAI;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeFlixBackend.Application.DTOs;
namespace AnimeFlixBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewSummarizerController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly AppDbContext _context;
        // 1. Declare the private fields
        private readonly IJikanService _jikanService;
        private readonly Client _geminiClient;
        public ReviewSummarizerController(IBackgroundJobClient backgroundJobClient, AppDbContext context,Client geminiClient, IJikanService jikanService )
        {
            _backgroundJobClient = backgroundJobClient;
            _context = context;
            _geminiClient = geminiClient;
            _jikanService= jikanService;
        }



        //[HttpPost("generate-directly/{malId}")]
        //public async Task<IActionResult> GenerateSummaryDirectly(int malId)
        //{
        //    try
        //    {
        //        // Now _jikanService will be recognized
        //        var reviews = await _jikanService.GetAnimeReviewsAsync(malId);

        //        if (reviews == null || !reviews.Any())
        //        {
        //            return NotFound(new { Message = "No reviews found to summarize." });
        //        }

        //        var reviewContent = string.Join(" ", reviews.Take(5).Select(r => r.Content));
        //        var prompt = $"Summarize these anime reviews into one concise paragraph: {reviewContent}";

        //        // Now _geminiClient will be recognized
        //        var response = await _geminiClient.Models.GenerateContentAsync("gemini-2.5-flash", prompt);
        //        var summaryText = response.Candidates[0].Content.Parts[0].Text;

        //        // Save to DB
        //        var existing = await _context.Reviews
        //            .FirstOrDefaultAsync(r => r.AnimeApiId == malId && r.UserId == 1);

        //        if (existing != null)
        //        {
        //            existing.ReviewText = summaryText;
        //            existing.CreatedAt = DateTime.UtcNow;
        //        }
        //        else
        //        {
        //            _context.Reviews.Add(new Review
        //            {
        //                UserId = 1,
        //                AnimeApiId = malId,
        //                ReviewText = summaryText,
        //                CreatedAt = DateTime.UtcNow
        //            });
        //        }
        //        await _context.SaveChangesAsync();

        //        return Ok(new { Summary = summaryText });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = ex.Message });
        //    }
        //}

        [HttpPost("summarize/{malId}")]
        public async Task<IActionResult> SummarizeSpecificAnime(int malId)
        {
            try
            {
                // 1. Fetch reviews for the specific anime ID
                var reviews = await _jikanService.GetAnimeReviewsAsync(malId);

                if (reviews == null || !reviews.Any())
                {
                    return NotFound(new { Message = $"No reviews found for anime ID {malId}." });
                }

                // 2. Format the reviews for the AI prompt
                // We take up to 5 reviews to give Gemini more context for a specific show
                var reviewText = string.Join("\n\n", reviews.Take(5).Select(r => r.Content));

                var prompt = $"Please provide a concise summary of the community consensus for this anime based on these reviews. " +
                             $"Highlight what people liked and what they disliked:\n\n{reviewText}";

                // 3. Call Gemini
                var response = await _geminiClient.Models.GenerateContentAsync("gemini-2.5-flash", prompt);
                var summaryResult = response.Candidates[0].Content.Parts[0].Text;

                return Ok(new
                {
                    MalId = malId,
                    Summary = summaryResult,
                    ReviewCountUsed = reviews.Take(5).Count(),
                    GeneratedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Processing failed", Details = ex.Message });
            }
        }
        /// <summary>
        /// Retrieves ONLY the AI consensus summary from the local database.
        /// </summary>
        [HttpGet("status/{malId}")]
        public async Task<IActionResult> GetAiSummaryStatus(int malId)
        {
            // Look for the review created by the System User (ID 1)
            var summary = await _context.Reviews
                .Where(r => r.AnimeApiId == malId && r.UserId == 1)
                .Select(r => new
                {
                    r.ReviewText,
                    r.CreatedAt,
                    Status = "Completed",
                    Provider = "Gemini 1.5 Flash"
                })
                .FirstOrDefaultAsync();

            if (summary == null)
            {
                return NotFound(new
                {
                    Message = "No AI summary found for this anime. Use the 'trigger' endpoint to start the process.",
                    MalId = malId
                });
            }

            return Ok(summary);
        }
    }
}