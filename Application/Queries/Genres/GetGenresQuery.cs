using Application.Models.Genres;
using MediatR;

namespace Application.Queries.Genres
{
    public class GetGenresQuery : IRequest<GenreDto[]>
    {
        public int[] GenreIds { get; set; }

        public string GenreName { get; set; }
    }
}
