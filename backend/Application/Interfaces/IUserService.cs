using AnimeFlixBackend.Domain.Entities;

namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IUserService
    {
            Task<User?> GetByEmailAsync(string email);
            Task<User?> GetByIdAsync(int id);
            Task<User?> GetByUsernameAsync(string username);
            Task<User> CreateAsync(User user);
            Task UpdateAsync(User user);
            Task<bool> ExistsAsync(string email);
        
    }
}
