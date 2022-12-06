using Application.Models.Genres;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers.Genres
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile() 
        {
            CreateMap<Genre, GenreDto>();
        }
    }
}
