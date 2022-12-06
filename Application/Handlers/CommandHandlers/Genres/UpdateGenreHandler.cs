using Application.Commands.Genres;
using Application.Exceptions;
using Application.Mappers.Genres;
using Application.Models.Genres;
using Core.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Genres
{
    public class UpdateGenreHandler : IRequestHandler<UpdateGenreCommand, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException("Необходимо указать название жанра");
            }

            var genreId = request.Id;

            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
            {
                throw new NotFoundException("Жанр не найден");
            }

            var query = _genreRepository.GetQuery()
                .Where(x => x.Id != genreId)
                .Where(x => x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase));

            if (query.Any())
            {
                throw new ConflictException("Жанр с таким названием уже существует");
            }

            genre.Name = request.Name;

            await _genreRepository.UpdateAsync(genre);
            await _genreRepository.Commit();

            var mapper = GenreMapper.Mapper;

            return mapper.Map<GenreDto>(genre);
        }
    }
}
