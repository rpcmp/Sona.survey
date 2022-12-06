using Application.Responses.Books;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers.Books
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile() 
        {
            CreateMap<Book, BookDto>();
        }
    }
}
