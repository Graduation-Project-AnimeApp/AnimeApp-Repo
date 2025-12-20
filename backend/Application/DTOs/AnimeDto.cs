using System.Text.Json.Serialization;

namespace AnimeFlixBackend.Application.DTOs
{
    public class AnimeDto
    {
        public int MalId { get; set; }
        public string Url { get; set; }

        public string Title { get; set; }
        public string? TitleEnglish { get; set; }
        public string? TitleJapanese { get; set; }
        public List<TitleDto>? Titles { get; set; }

        public string Type { get; set; }
        public string Source { get; set; }
        public int Episodes { get; set; }
        public string Status { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }

        public double Score { get; set; }
        public int ScoredBy { get; set; }
        public int Rank { get; set; }
        public int Popularity { get; set; }
        public int Members { get; set; }
        public int Favorites { get; set; }

        public string? Synopsis { get; set; }
        public string? Background { get; set; }
        public string? Season { get; set; }
        public int? Year { get; set; }

        public ImagesDto Images { get; set; }
        public TrailerDto? Trailer { get; set; }

        public AirDateDto? Aired { get; set; }
        public BroadcastDto? Broadcast { get; set; }

        public List<GenreDto>? Genres { get; set; }
        public List<GenreDto>? Themes { get; set; }
        public List<GenreDto>? Demographics { get; set; }
        public List<GenreDto>? Studios { get; set; }
        public List<GenreDto>? Producers { get; set; }

        public bool IsInWatchlist { get; set; }
        public bool IsWatched { get; set; }
        public double? AiScore { get; set; }
        public string? RecommendationReason { get; set; }
    }
    public class ImagesDto
    {
        public ImageSizeDto Jpg { get; set; }
        public ImageSizeDto Webp { get; set; }
    }

    public class ImageSizeDto
    {
        [JsonPropertyName("image_url")]

        public string ImageUrl { get; set; }
        [JsonPropertyName("small_image_url")]

        public string SmallImageUrl { get; set; }
        [JsonPropertyName("large_image_url")]

        public string LargeImageUrl { get; set; }
    }

    public class TrailerDto
    {
        public string? Url { get; set; }
    }

    public class AirDateDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? String { get; set; }
    }

    public class BroadcastDto
    {
        public string? Day { get; set; }
        public string? Time { get; set; }
        public string? Timezone { get; set; }
    }

    public class GenreDto
    {
        public int MalId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
    public class TitleDto
    {
        public string Type { get; set; }
        public string Title { get; set; }
    }


}
