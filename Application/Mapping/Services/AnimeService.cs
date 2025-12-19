using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Infrastructure.External;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeFlixBackend.Application.Mapping.Services
{
    // The concrete service implementing the interface
    public class AnimeService : IAnimeService
    {
        private readonly IJikanService _jikanService;
        private readonly IMapper _mapper;

        public AnimeService(IJikanService jikanService, IMapper mapper)
        {
            _jikanService = jikanService;
            _mapper = mapper;
        }

        // Implements the original method to get a single anime by ID
        public async Task<AnimeDto?> GetAnimeAsync(int malId)
        {
            var jikanAnime = await _jikanService.GetAnimeByMalIdAsync(malId);

            // Map the Jikan API model to your Application DTO
            return _mapper.Map<AnimeDto>(jikanAnime);
        }

        /// <summary>
        /// Searches anime using the Jikan API's /anime endpoint with pagination.
        /// </summary>
        public async Task<List<AnimeDto>> SearchAnimeAsync(string? query, int page)
        {
            // 1. Call the Jikan service to get the raw list data
            // Note: This requires you to implement a SearchAnimeAsync method in IJikanService 
            // that handles the 'q' parameter and pagination, returning JikanListResponse<JikanAnime>.

            // For now, we'll proxy the call to the general GetAllAnimeAsync to fetch a page
            // A dedicated search method in IJikanService would be more precise.
            var jikanAnimeList = await _jikanService.SearchAnimeAsync(query, page);

            if (jikanAnimeList == null || !jikanAnimeList.Any())
            {
                return new List<AnimeDto>();
            }

            // 2. Map the list of raw Jikan models to the list of Anime DTOs
            return _mapper.Map<List<AnimeDto>>(jikanAnimeList);
        }

        /// <summary>
        /// Retrieves the latest/popular anime using a dedicated Jikan endpoint (e.g., /top/anime or /seasons/now).
        /// </summary>
        public async Task<List<AnimeDto>> GetLatestAnimeAsync()
        {
            // Note: This requires you to implement a GetLatestAnimeAsync method in IJikanService
            // that calls a specific endpoint like /v4/seasons/now.

            var jikanAnimeList = await _jikanService.GetLatestAnimeAsync();

            if (jikanAnimeList == null || !jikanAnimeList.Any())
            {
                return new List<AnimeDto>();
            }

            // 2. Map the list of raw Jikan models to the list of Anime DTOs
            return _mapper.Map<List<AnimeDto>>(jikanAnimeList);
        }
    }
}