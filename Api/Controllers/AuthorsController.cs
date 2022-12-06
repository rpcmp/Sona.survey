using Application.Commands.Authors;
using Application.Models.Authors;
using Application.Queries.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<AuthorDto> GetAuthor([FromRoute] int id)
        {
            var query = new GetAuthorsQuery()
            {
                AuthorIds = new[] { id },
            };

            var result = await _mediator.Send(query);
            return result.FirstOrDefault();
        }

        [HttpGet]
        public async Task<AuthorDto[]> GetAuthors(int[] authorIds, string authorFirstName, string authorLastName)
        {
            var query = new GetAuthorsQuery()
            {
                AuthorIds = authorIds,
                AuthorFirstName = authorFirstName,
                AuthorLastName = authorLastName
            };

            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPut]
        public async Task<AuthorDto> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost]
        public async Task<AuthorDto> UpdateAuthor([FromBody] UpdateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAuthor([FromRoute] int id)
        {
            var command = new DeleteAuthorCommand() 
            { 
                Id = id 
            };

            await _mediator.Send(command);
        }
    }
}
