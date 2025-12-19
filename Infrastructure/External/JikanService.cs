using System.Net.Http.Json;
using AnimeFlixBackend.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace AnimeFlixBackend.Infrastructure.External
{
    // Note: You must define JikanAnime, IJikanService, JikanSingleResponse<T>, 
    // and JikanListResponse<T> with PaginationDto for this file to compile.

    public class JikanService : IJikanService
    {
        private readonly HttpClient _httpClient;
        private static readonly SemaphoreSlim _rateLimiter = new SemaphoreSlim(1, 1);
        private const int RequestDelayMilliseconds = 500;

        public JikanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JikanAnime?> GetAnimeByMalIdAsync(int malId)
        {
            await ApplyRateLimitAsync();
            var response =
                await _httpClient.GetFromJsonAsync<JikanSingleResponse<JikanAnime>>(
                    $"anime/{malId}");
            return response?.Data;
        }

        // ----------------------------------------------------------------------
        // 💡 NEW IMPLEMENTATION 1: Search and Paginated List Retrieval
        // ----------------------------------------------------------------------
        public async Task<List<JikanAnime>> SearchAnimeAsync(string? query, int page)
        {
            // 1. Enforce rate limit
            await ApplyRateLimitAsync();

            // 2. Build the query URL:
            // Use query parameters 'q' for search, and 'page' for pagination.
            var endpoint = $"anime?page={page}&limit=25";

            if (!string.IsNullOrWhiteSpace(query))
            {
                // Jikan API expects the search query to be URL encoded
                endpoint += $"&q={Uri.EscapeDataString(query)}";
            }

            // Default sort to maintain consistency when no query is provided
            endpoint += "&order_by=mal_id&sort=asc";

            // 3. Execute the HTTP request
            var response = await _httpClient.GetFromJsonAsync<JikanListResponse<JikanAnime>>(endpoint);

            // 4. Return the list for the current page, or an empty list if null/empty
            return response?.Data ?? new List<JikanAnime>();
        }

        // ----------------------------------------------------------------------
        // 💡 NEW IMPLEMENTATION 2: Get Latest/Trending Anime
        // ----------------------------------------------------------------------
        public async Task<List<JikanAnime>> GetLatestAnimeAsync()
        {
            // 1. Enforce rate limit
            await ApplyRateLimitAsync();

            // 2. Build the endpoint for current season or top anime
            // We'll use /v4/seasons/now for the "latest" airing anime
            var endpoint = "seasons/now?limit=25";

            // 3. Execute the HTTP request
            var response = await _httpClient.GetFromJsonAsync<JikanListResponse<JikanAnime>>(endpoint);

            // 4. Return the list, or an empty list if null/empty
            return response?.Data ?? new List<JikanAnime>();
        }

        // ----------------------------------------------------------------------
        // Existing heavy method - Kept for reference, but SearchAnimeAsync is often preferred by controllers
        // ----------------------------------------------------------------------
        public async Task<List<JikanAnime>> GetAllAnimeAsync()
        {
            // ... (Your existing GetAllAnimeAsync loop logic) ...
            var allAnime = new List<JikanAnime>();
            int page = 1;
            bool hasNextPage = true;

            while (hasNextPage)
            {
                await ApplyRateLimitAsync();
                var endpoint = $"anime?page={page}&limit=25&order_by=mal_id&sort=asc";
                var response = await _httpClient.GetFromJsonAsync<JikanListResponse<JikanAnime>>(endpoint);

                if (response != null && response.Data != null && response.Data.Any())
                {
                    allAnime.AddRange(response.Data);
                    hasNextPage = response.Pagination.HasNextPage;
                    page++;
                }
                else
                {
                    hasNextPage = false;
                }
            }

            return allAnime;
        }

        /// <summary>
        /// Uses a SemaphoreSlim to enforce a minimum delay between Jikan API calls.
        /// </summary>
        private static async Task ApplyRateLimitAsync()
        {
            await _rateLimiter.WaitAsync();
            try
            {
                await Task.Delay(RequestDelayMilliseconds);
            }
            finally
            {
                _rateLimiter.Release();
            }
        }
        public async Task<List<JikanReview>> GetAnimeReviewsAsync(int malId)
        {
            var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/anime/{malId}/reviews");

            if (!response.IsSuccessStatusCode) return new List<JikanReview>();

            var result = await response.Content.ReadFromJsonAsync<JikanReviewResponse>();
            return result?.Data ?? new List<JikanReview>();
        }
    }
}