using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace AnimeFlixBackend.Application.Mapping.Services
{
  public class UserService : IUserService
   {
      private readonly AppDbContext _context;

      public UserService(AppDbContext context)
      {
          _context = context;
      }

      public async Task<User?> GetByEmailAsync(string email)
      {
            return await _context.Users.Include(u => u.Token).FirstOrDefaultAsync(u => u.Email == email);
      }

      public async Task<User?> GetByIdAsync(int id)
      {
         return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
      }

       public async Task<User?> GetByUsernameAsync(string username)
       {
          return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
       }

       public async Task<User> CreateAsync(User user)
       {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
       }

       public async Task UpdateAsync(User user)
       {
           _context.Users.Update(user);
           await _context.SaveChangesAsync();
       }
     
       public async Task<bool> ExistsAsync(string email)
       {
           return await _context.Users.AnyAsync(u => u.Email == email);
       }
    }
}
