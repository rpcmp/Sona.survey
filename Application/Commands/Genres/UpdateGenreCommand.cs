using Application.Models.Genres;
using MediatR;

namespace Application.Commands.Genres
{
    public class UpdateGenreCommand : IRequest<GenreDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
