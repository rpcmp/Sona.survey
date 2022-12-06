using Application.Commands.Genres;
using Application.Exceptions;
using Application.Mappers.Genres;
using Application.Models.Genres;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Genres
{
    public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public CreateGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException("Необходимо указать название жанра");
            }

            var query = _genreRepository.GetQuery()
                .Where(x => x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase));

            if (query.Any())
            {
                throw new ConflictException("Жанр с таким названием уже существует");
            }

            var genre = new Genre()
            {
                Name = request.Name
            };

            await _genreRepository.AddAsync(genre);
            await _genreRepository.Commit();

            var mapper = GenreMapper.Mapper;

            return mapper.Map<GenreDto>(genre);
        }
    }
}
