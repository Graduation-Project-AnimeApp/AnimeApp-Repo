using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFlixBackend.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchHistoryController : ControllerBase
    {
        private readonly IWatchHistoryService _historyService;

        // Dependency Injection - IWatchHistoryService is injected by the DI container
        public WatchHistoryController(IWatchHistoryService historyService)
        {
            _historyService = historyService;
        }

        /// <summary>
        /// Retrieves the complete watch history for a specific user.
        /// </summary>
        // GET: api/WatchHistory/user/5
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<WatchHistoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserHistory(int userId)
        {
            // Note: Best practice is to validate the requested userId against the authenticated user's ID
            if (userId <= 0)
            {
                return BadRequest("Invalid User ID.");
            }

            var history = await _historyService.GetUserHistory(userId);

            if (history == null || !history.Any())
            {
                // Return an empty list if no history is found, which is a common pattern for lists
                return Ok(new List<WatchHistoryDto>());
            }

            return Ok(history);
        }

        /// <summary>
        /// Marks an anime as watched for the given user, adding it to their history.
        /// </summary>
        // POST: api/WatchHistory/add
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToHistory([FromBody] WatchHistoryRequest request)
        {
            // Ensure necessary data is present and valid
            if (request.UserId <= 0 || request.AnimeApiId <= 0)
            {
                return BadRequest("User ID and Anime API ID must be valid.");
            }

            await _historyService.AddAsync(request.UserId, request.AnimeApiId);

            // 204 No Content indicates success without a body
            return NoContent();
        }
    }
}

// ⚠️ You'll need this DTO for the POST request body.
// It should be defined in a DTO namespace, perhaps a new 'Requests' folder.
public class WatchHistoryRequest
{
    public int UserId { get; set; }
    public int AnimeApiId { get; set; }
}