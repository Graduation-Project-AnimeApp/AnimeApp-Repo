using AnimeFlix.Data;
using AnimeFlixBackend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions; // Needed for Expression<Func<T, bool>>

namespace AnimeFlixBackend.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- NEW: Implementation of FindAsync ---
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // Applies the filter predicate and executes the query in the database.
            return await _context.Set<T>()
                                 .Where(predicate)
                                 .ToListAsync();
        }

        // ... (existing methods GetByIdAsync, GetAllAsync, AddAsync, Delete, Update) ...
        public async Task<T?> GetByIdAsync(int id)
             => await _context.Set<T>().FindAsync(id);

        public async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}