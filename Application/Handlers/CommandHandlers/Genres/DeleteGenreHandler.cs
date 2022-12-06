using Application.Commands.Genres;
using Application.Exceptions;
using Core.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Genres
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IBookRepository _bookRepository;

        public DeleteGenreHandler(IGenreRepository genreRepository, IBookRepository bookRepository)
        {
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genreId = request.Id;

            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
            {
                throw new NotFoundException("Жанр не найден");
            }

            var bookQuery = _bookRepository.GetQuery().Where(x => x.Genre.Id == genreId);
            if (bookQuery.Any())
            {
                throw new ConflictException("У данного жанра есть книги. Перед удалением жанра необходимо удалить книги с этим жанром");
            }

            await _genreRepository.DeleteAsync(genre);
            await _genreRepository.Commit();

            return Unit.Value;
        }
    }
}
