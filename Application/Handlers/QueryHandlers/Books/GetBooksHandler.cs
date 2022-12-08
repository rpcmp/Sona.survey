using Application.Mappers.Books;
using Application.Queries.Books;
using Application.Responses.Books;
using Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers.Books
{
    public class GetBooksHandler : IRequestHandler<GetBooksQuery, BookDto[]>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<BookDto[]> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _bookRepository.GetQuery()
                .Include(x => x.Genre)
                .Include(x => x.Author)
                .AsQueryable();

            if (request.BookIds != null && request.BookIds.Any())
            {
                query = query.Where(x => request.BookIds.Contains(x.Id));
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.GenreName)) 
            {
                query = query.Where(x => x.Genre.Name.Contains(request.GenreName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.AuthorFirstName))
            {
                query = query.Where(x => x.Author.FirstName.Contains(request.AuthorFirstName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.AuthorLastName))
            {
                query = query.Where(x => x.Author.LastName.Contains(request.AuthorLastName, StringComparison.InvariantCultureIgnoreCase));
            }

            var mapper = BookMapper.Mapper;

            var books = query.Select(x => mapper.Map<BookDto>(x)).ToArray();
            return Task.FromResult(books);
        }
    }
}
