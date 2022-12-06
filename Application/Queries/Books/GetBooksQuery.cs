using Application.Responses.Books;
using MediatR;

namespace Application.Queries.Books
{
    public class GetBooksQuery : IRequest<BookDto[]>
    {
        public int[] BookIds { get; set; }

        public string Title { get; set; }

        public string GenreName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
