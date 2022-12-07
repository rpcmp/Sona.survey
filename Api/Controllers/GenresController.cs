using Application.Commands.Genres;
using Application.Models.Genres;
using Application.Queries.Genres;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<GenreDto> GetGenre([FromRoute] int id)
        {
            var query = new GetGenresQuery()
            {
                GenreIds = new[] { id },
            };

            var result = await _mediator.Send(query);
            return result.FirstOrDefault();
        }

        [Authorize]
        [HttpGet]
        public async Task<GenreDto[]> GetGenres([FromQuery] int[] genreIds, [FromQuery] string genreName)
        {
            var query = new GetGenresQuery()
            {
                GenreIds = genreIds,
                GenreName = genreName
            };

            var result = await _mediator.Send(query);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<GenreDto> CreateGenre([FromBody] CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<GenreDto> UpdateGenre([FromBody] UpdateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete([FromQuery] int id)
        {
            var command = new DeleteGenreCommand()
            {
                Id = id
            };

            await _mediator.Send(command);
        }
    }
}
