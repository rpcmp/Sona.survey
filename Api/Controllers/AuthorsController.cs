using Application.Commands.Authors;
using Application.Models.Authors;
using Application.Queries.Authors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task<AuthorDto[]> GetAuthors([FromQuery] int[] authorIds, [FromQuery] string authorFirstName, [FromQuery] string authorLastName)
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

        [Authorize]
        [HttpPut]
        public async Task<AuthorDto> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<AuthorDto> UpdateAuthor([FromBody] UpdateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [Authorize]
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
