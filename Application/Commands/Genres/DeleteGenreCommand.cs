using MediatR;

namespace Application.Commands.Genres
{
    public class DeleteGenreCommand : IRequest
    {
        public int Id { get; set; }
    }
}
