using AnimeFlix.Data;
using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using static AnimeFlixBackend.Application.Mapping.Services.AuthService;

namespace AnimeFlixBackend.Application.Mapping.Services
{
    public class AuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly AppDbContext _context;  

            public AuthService(
                IUserService userService,
                IJwtService jwtService,
                AppDbContext context) 
            {
                _userService = userService;
                _jwtService = jwtService;
                _context = context;  
            }

            public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
            {
                // Check if user exists
                if (await _userService.ExistsAsync(request.Email))
                {
                    throw new Exception("User with this email already exists");
                }

                // Check if username is taken
                if (await _userService.GetByUsernameAsync(request.Username) != null)
                {
                    throw new Exception("Username is already taken");
                }

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    CreatedAt = DateTime.UtcNow
                };

                user = await _userService.CreateAsync(user);

                // Generate tokens
                var accessToken = _jwtService.GenerateAccessToken(user);

                var authToken = new AuthToken
                {
                    TokenName = accessToken,
                    TokenExpirationDate = DateTime.UtcNow.AddDays(7),
                    UserId = user.UserId,
                };

                // Save AuthToken using DbContext
                _context.Tokens.Add(authToken);
                await _context.SaveChangesAsync();

                return new AuthResponseDto
                {
                    Token = accessToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    User = new UserDto
                    {
                        Id = user.UserId,
                        Username = user.Username,
                        Email = user.Email
                    }
                };
            }
        
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            // Find user
            var user = await _userService.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            var accessToken = _jwtService.GenerateAccessToken(user);

            // Save refresh token
            if (user.Token != null)
            {
                user.Token.TokenExpirationDate = DateTime.UtcNow.AddDays(7);
            }
            else
            {
                user.Token = new AuthToken
                {
                    TokenName = accessToken,
                    TokenExpirationDate = DateTime.UtcNow.AddDays(7),
                    UserId = user.UserId
                };
            }

            await _userService.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token = accessToken,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                User = new UserDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    Email = user.Email
                }
            };
        }
    }
}