using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFlixBackend.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        /// <summary>
        /// Retrieves a personalized list of anime recommendations for the authenticated user.
        /// </summary>
        /// <param name="userId">The ID of the user requesting recommendations (should come from auth claims).</param>
        // GET: api/Recommendation/user/5
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<AnimeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecommendations(int userId)
        {
            // Note: In a real production environment, you should always get the 
            // userId from the authenticated user's token/claims, not from the URL path, 
            // to prevent one user from querying another user's recommendations.

            if (userId <= 0)
            {
                return BadRequest("Invalid User ID.");
            }

            var recommendations = await _recommendationService.GetRecommendations(userId);

            if (recommendations == null || !recommendations.Any())
            {
                // Return 404 if no recommendations could be generated (e.g., brand new user)
                return NotFound("No recommendations found for this user.");
            }

            return Ok(recommendations);
        }
    }
}