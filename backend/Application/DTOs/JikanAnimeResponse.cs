using System.Text.Json.Serialization;

namespace AnimeFlixBackend.Application.DTOs
{

    public class JikanSingleResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
    public class JikanImageSize
    {
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("small_image_url")]
        public string SmallImageUrl { get; set; }

        [JsonPropertyName("large_image_url")]
        public string LargeImageUrl { get; set; }
    }


    public class JikanImages
    {
        [JsonPropertyName("jpg")]
        public JikanImageSize Jpg { get; set; }

        [JsonPropertyName("webp")]
        public JikanImageSize Webp { get; set; }
    }



    public class JikanTrailer
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }



    public class JikanAired
    {
        [JsonPropertyName("from")]
        public string? From { get; set; }

        [JsonPropertyName("to")]
        public string? To { get; set; }

        [JsonPropertyName("string")] 
        public string? String { get; set; }
    }



    public class JikanBroadcast
    {
        [JsonPropertyName("day")]
        public string? Day { get; set; }

        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }
    }
    public class JikanGenre
    {
        [JsonPropertyName("mal_id")]
        public int Mal_Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class JikanTitle
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
    public class JikanReview
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("entry")] 
        public JikanReviewEntry Entry { get; set; }
    }

    public class JikanReviewEntry
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }
    }
}
