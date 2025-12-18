using AnimeFlixBackend.Application.DTOs;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface ICharacterChatService
    {
        Task<string> GetCharacterResponseAsync(ChatRequest request);
    }
}
