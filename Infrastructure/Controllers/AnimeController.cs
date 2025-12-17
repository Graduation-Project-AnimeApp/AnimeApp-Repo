using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimeFlixBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        /// <summary>
        /// Retrieves detailed information for a single anime by its MyAnimeList ID (MAL ID).
        /// </summary>
        /// <param name="malId">The unique ID of the anime in the external database.</param>
        [HttpGet("{malId:int}")]
        public async Task<ActionResult<AnimeDto>> GetAnime(int malId)
        {
            var animeDto = await _animeService.GetAnimeAsync(malId);

            if (animeDto == null)
            {
                // Assuming you have implemented the custom NotFoundException middleware
                // If not, you can return NotFound() directly.
                return NotFound($"Anime with ID {malId} not found.");
            }

            return Ok(animeDto);
        }

        /// <summary>
        /// Retrieves a list of anime based on optional search queries and pagination.
        /// This is the endpoint to get results resembling 'all anime' via pagination.
        /// </summary>
        /// <param name="query">Optional search query (e.g., "Attack on Titan").</param>
        /// <param name="page">The page number for results (default 1).</param>
        /// <returns>A list of paginated AnimeDto objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeDto>>> SearchAnime(
            [FromQuery] string? query,
            [FromQuery] int page = 1)
        {
            // Note: The actual implementation of IAnimeService.SearchAnimeAsync 
            // should handle the pagination logic and mapping from Jikan's ListResponse<T>.
            var animeList = await _animeService.SearchAnimeAsync(query, page);

            if (animeList == null || !animeList.Any())
            {
                return NotFound("No anime results found for the given query.");
            }

            return Ok(animeList);
        }

        /// <summary>
        /// Retrieves a list of the most recent or popular anime (e.g., for a homepage feed).
        /// </summary>
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<AnimeDto>>> GetLatestAnime()
        {
            // This service method would call Jikan's /v4/seasons/now or /v4/top/anime endpoint
            var latest = await _animeService.GetLatestAnimeAsync();

            if (latest == null || !latest.Any())
            {
                return NotFound("Could not retrieve latest anime list.");
            }

            return Ok(latest);
        }
    }
}