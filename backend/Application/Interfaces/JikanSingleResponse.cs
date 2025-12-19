using System.Text.Json.Serialization;

namespace AnimeFlixBackend.Application.Interfaces
{

    public class JikanSingleResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }

}
