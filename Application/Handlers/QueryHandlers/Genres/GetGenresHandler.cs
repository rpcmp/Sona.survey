using Application.Mappers.Genres;
using Application.Models.Genres;
using Application.Queries.Genres;
using Core.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers.Genres
{
    public class GetGenresHandler : IRequestHandler<GetGenresQuery, GenreDto[]>
    {
        private readonly IGenreRepository _genreRepository;

        public GetGenresHandler(IGenreRepository genreRepository) 
        {
            _genreRepository = genreRepository;
        }

        public Task<GenreDto[]> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var query = _genreRepository.GetQuery();

            if (request.GenreIds != null && request.GenreIds.Any())
            {
                query = query.Where(x => request.GenreIds.Contains(x.Id));
            }

            if (!string.IsNullOrEmpty(request.GenreName))
            {
                query = query.Where(x => x.Name.Contains(request.GenreName, StringComparison.InvariantCultureIgnoreCase));
            }

            var mapper = GenreMapper.Mapper;

            var result = query.Select(x => mapper.Map<GenreDto>(x)).ToArray();
            return Task.FromResult(result);
        }
    }
}
