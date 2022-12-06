using Application.Mappers.Authors;
using Application.Mappers.Genres;
using AutoMapper;
using System;

namespace Application.Mappers.Books
{
    public static class BookMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookMappingProfile>();
                cfg.AddProfile<GenreMappingProfile>();
                cfg.AddProfile<AuthorMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
