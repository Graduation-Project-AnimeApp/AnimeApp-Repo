using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using Google.GenAI;
using Google.GenAI.Types;

namespace AnimeFlixBackend.Application.Services
{
    public class CharacterChatService : ICharacterChatService
    {
        private readonly Client _client;

        public CharacterChatService(Client client)
        {
            _client = client;
        }

        public async Task<string> GetCharacterResponseAsync(ChatRequest request)
        {
            // 1. Prepare conversation history
            // IMPORTANT: Only "user" and "model" roles are allowed here.
            var contents = new List<Content>();

            foreach (var msg in request.History)
            {
                contents.Add(new Content
                {
                    // Map "assistant" or "bot" to "model" to keep Gemini happy
                    Role = msg.Role.ToLower() == "user" ? "user" : "model",
                    Parts = [new Part { Text = msg.Text }]
                });
            }

            // Add the current user message
            contents.Add(new Content
            {
                Role = "user",
                Parts = [new Part { Text = request.UserMessage }]
            });

            // 2. Define the Character Persona
            var systemPrompt = $"You are {request.CharacterName}. " +
                               "Stay fully in character. Use their tone, personality, and catchphrases. " +
                               "Never mention being an AI.";

            // 3. Make the API call
            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash", // Using the latest 2025 model
                contents: contents,
                config: new GenerateContentConfig
                {
                    // This is why the Business Controller worked!
                    SystemInstruction = new Content
                    {
                        Parts = [new Part { Text = systemPrompt }]
                    },
                    Temperature = 0.8f,
                }
            );

            // 4. Extract and return text
            return response.Candidates.First().Content.Parts.First().Text;
        }
    }
}