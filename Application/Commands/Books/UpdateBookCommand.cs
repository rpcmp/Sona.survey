using Application.Responses.Books;
using MediatR;

namespace Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<BookDto>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public uint Year { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }
    }
}
