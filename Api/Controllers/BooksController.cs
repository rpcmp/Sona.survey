using Application.Commands.Books;
using Application.Queries.Books;
using Application.Responses.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<BookDto> GetBook([FromRoute] int id)
        {
            var query = new GetBooksQuery()
            {
                BookIds = new[] { id },
            };

            var result = await _mediator.Send(query);
            return result.FirstOrDefault();
        }

        [HttpGet]
        public async Task<BookDto[]> GetBooks(int[] bookIds, string title, string genreName, string authorFirstName, string authorLastName)
        {
            var query = new GetBooksQuery()
            {
                BookIds = bookIds,
                Title = title,
                GenreName = genreName,
                AuthorFirstName = authorFirstName,
                AuthorLastName = authorLastName
            };

            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPut]
        public async Task<BookDto> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost]
        public async Task<BookDto> UpdateBook([FromBody] UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task DeleteBook([FromRoute] int id)
        {
            var command = new DeleteBookCommand()
            {
                Id = id
            };

            await _mediator.Send(command);
        }
    }
}
