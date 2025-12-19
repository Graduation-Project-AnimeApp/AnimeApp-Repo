using System.Text.Json.Serialization;

namespace AnimeFlixBackend.Application.DTOs
{
    public class ReviewDto
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("entry")] // This is the missing piece
        public ReviewEntryDto Entry { get; set; }
    }

    public class ReviewEntryDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }
    }

}
