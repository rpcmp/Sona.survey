using Application.Models.Authors;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers.Authors
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}
