using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using AnimeFlixBackend.Domain.Enums;
using AutoMapper;

namespace AnimeFlixBackend.Application.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WatchlistService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddAsync(int userId, int animeApiId)
        {
            var entity = new Watchlist
            {
                UserId = userId,
                AnimeApiId = animeApiId
            };

            await _uow.Watchlists.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task<List<WatchlistDto>> GetUserWatchlist(int userId)
        {
            var data = await _uow.Watchlists.GetAllAsync();
            return _mapper.Map<List<WatchlistDto>>(
                data.Where(x => x.UserId == userId));
        }
    }

}
