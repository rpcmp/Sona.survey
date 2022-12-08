using Application.Models.Authors;
using Application.Models.Genres;
using Application.Responses.Books;
using MediatR;

namespace Application.Commands.Books
{
    public class CreateBookCommand : IRequest<BookDto>
    {
        public string Title { get; set; }

        public uint Year { get; set; }

        public AuthorDto Author { get; set; }

        public GenreDto Genre { get; set; }
    }
}
