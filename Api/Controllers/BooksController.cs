using Application.Commands.Books;
using Application.Queries.Books;
using Application.Responses.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task<BookDto[]> GetBooks([FromQuery] int[] bookIds, [FromQuery] string title, [FromQuery] string genreName, [FromQuery] string authorFirstName, [FromQuery] string authorLastName)
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

        [Authorize]
        [HttpPut]
        public async Task<BookDto> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<BookDto> UpdateBook([FromBody] UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [Authorize]
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
