using Application.Responses.Books;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers.Books
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(x => x.Genre, opt => opt.MapFrom(x => x.Genre))
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author));
        }
    }
}
