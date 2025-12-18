using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Application.Interfaces;
using AnimeFlixBackend.Domain.Entities;
using AutoMapper;

namespace AnimeFlixBackend.Application.Mapping.Services
{
    public class WatchHistoryService : IWatchHistoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WatchHistoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddAsync(int userId, int animeApiId)
        {
            await _uow.WatchHistories.AddAsync(new WatchHistory
            {
                UserId = userId,
                AnimeApiId = animeApiId
            });

            await _uow.SaveChangesAsync();
        }

        public async Task<List<WatchHistoryDto>> GetUserHistory(int userId)
        {
            var data = await _uow.WatchHistories.FindAsync(x => x.UserId == userId);

            return _mapper.Map<List<WatchHistoryDto>>(data);
        }
    }

}
