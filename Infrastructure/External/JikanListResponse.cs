using AnimeFlixBackend.Application.DTOs;
using System.Text.Json.Serialization;

namespace AnimeFlixBackend.Infrastructure.External
{
    // Inside AnimeFlixBackend.Application.Interfaces or Infrastructure.External
    public class JikanListResponse<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        [JsonPropertyName("pagination")]
        public PaginationDto Pagination { get; set; }
    }
}
