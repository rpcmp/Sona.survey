using Application.Models.Genres;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Genres
{
    public class CreateGenreCommand : IRequest<GenreDto>
    {
        public string Name { get; set; }
    }
}
