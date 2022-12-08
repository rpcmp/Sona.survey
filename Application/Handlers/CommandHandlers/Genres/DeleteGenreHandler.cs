using Application.Commands.Genres;
using Application.Exceptions;
using Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Genres
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genreId = request.Id;

            var genre = _genreRepository.GetQuery()
                .Where(x => x.Id == genreId)
                .Include(x => x.Books)
                .FirstOrDefault();

            if (genre == null)
            {
                throw new NotFoundException("Жанр не найден");
            }

            if (genre.Books != null && genre.Books.Any())
            {
                throw new ConflictException("У данного жанра есть книги. Перед удалением жанра необходимо удалить книги с этим жанром");
            }

            await _genreRepository.DeleteAsync(genre);
            await _genreRepository.Commit();

            return Unit.Value;
        }
    }
}
