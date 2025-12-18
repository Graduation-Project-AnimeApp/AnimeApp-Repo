namespace AnimeFlixBackend.Application.DTOs
{
    public class ChatRequest
    {
        public string CharacterName { get; set; } = string.Empty;
        public string UserMessage { get; set; } = string.Empty;
        // Optional: Send previous history to keep the context
        public List<ChatMessageDto> History { get; set; } = new();
    }

    public class ChatMessageDto
    {
        public string Role { get; set; } = "user"; // "user" or "model"
        public string Text { get; set; } = string.Empty;
    }
}
