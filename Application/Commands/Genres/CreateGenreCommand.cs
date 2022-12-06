using Application.Models.Genres;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Genres
{
    public class CreateGenreCommand : IRequest<GenreDto>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо указать название жанра")]
        public string Name { get; set; }
    }
}
