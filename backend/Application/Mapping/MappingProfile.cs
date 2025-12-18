using AnimeFlixBackend.Application.DTOs;
using AnimeFlixBackend.Domain.Entities;
using AnimeFlixBackend.Infrastructure.External; // Assuming Jikan DTOs are here
using AutoMapper;

namespace AnimeFlixBackend.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- Domain to DTO Mappings ---
            CreateMap<Watchlist, WatchlistDto>();
            CreateMap<WatchHistory, WatchHistoryDto>();
            CreateMap<Review, ReviewDto>();

            // --- Jikan External DTO Mappings ---

            // 1. Primary Anime Map (JikanAnime to AnimeDto)
            // Note: AutoMapper handles identical PascalCase properties (Title, Type, etc.)
            CreateMap<JikanAnime, AnimeDto>()
                // Assuming JikanAnime C# properties are named MalId, TitleEnglish, etc.,
                // but if your JikanAnime class used the snake_case for C# properties
                // (e.g., public int Mal_Id { get; set; }), you would map them here:
                /* .ForMember(d => d.MalId, o => o.MapFrom(s => s.Mal_Id)) 
                   .ForMember(d => d.TitleEnglish, o => o.MapFrom(s => s.Title_English)) 
                   .ForMember(d => d.ScoredBy, o => o.MapFrom(s => s.Scored_By)); */
                ;

            // 2. Sub-DTO Mappings (Fixed Typo)
            CreateMap<JikanGenre, GenreDto>() // FIX: Changed JickanGenre to JikanGenre
                .ForMember(d => d.MalId, o => o.MapFrom(s => s.Mal_Id)); // Assuming external entity uses Mal_Id

            // Auto-mapped sub-types:
            CreateMap<JikanTitle, TitleDto>();
            CreateMap<JikanImageSize, ImageSizeDto>();
            CreateMap<JikanImages, ImagesDto>();
            CreateMap<JikanTrailer, TrailerDto>();
            CreateMap<JikanAired, AirDateDto>();
            CreateMap<JikanBroadcast, BroadcastDto>();
        }
    }
}