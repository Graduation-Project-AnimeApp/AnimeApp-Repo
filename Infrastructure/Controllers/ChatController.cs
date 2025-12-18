using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFlixBackend.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ICharacterChatService _chatService;

        public ChatController(ICharacterChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserMessage))
                return BadRequest("Message is required");

            var response = await _chatService.GetCharacterResponseAsync(request);

            return Ok(new
            {
                characterResponse = response
            });
        }
    }
}
