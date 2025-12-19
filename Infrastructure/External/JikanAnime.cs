
using AnimeFlixBackend.Application.DTOs;
using System.Text.Json.Serialization;

namespace AnimeFlixBackend.Infrastructure.External
{
    public class JikanAnime
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        public string Url { get; set; }
        public string Title { get; set; }

        [JsonPropertyName("title_english")]
        public string TitleEnglish { get; set; }

        [JsonPropertyName("title_japanese")]
        public string TitleJapanese { get; set; }

        public string Type { get; set; }
        public string Source { get; set; }
        public int? Episodes { get; set; }
        public string Status { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }

        public double? Score { get; set; }
        public int? ScoredBy { get; set; }
        public int? Rank { get; set; }
        public int? Popularity { get; set; }
        public int? Members { get; set; }
        public int? Favorites { get; set; }

        public string Synopsis { get; set; }
        public string Background { get; set; }
        public string Season { get; set; }
        public int? Year { get; set; }

        public ImagesDto Images { get; set; }
        public TrailerDto Trailer { get; set; }
        public AirDateDto Aired { get; set; }
        public BroadcastDto Broadcast { get; set; }

        public List<GenreDto> Genres { get; set; }
        public List<GenreDto> Studios { get; set; }
    }
    public class JikanReview
    {
        public string Content { get; set; } = string.Empty;
        public int Score { get; set; }
    }
    public class JikanReviewResponse
    {
        public List<JikanReview> Data { get; set; } = new();
    }
}
