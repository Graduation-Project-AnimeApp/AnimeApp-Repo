using AnimeFlixBackend.Domain.Entities;
using System.Security.Claims;

namespace AnimeFlixBackend.Application.Interfaces
{
        public interface IJwtService
        {
            string GenerateAccessToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
            int? GetUserIdFromToken(string token);
        }
    
}
