using Application.Mappers.Authors;
using Application.Models.Authors;
using Application.Queries.Authors;
using Core.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers.Authors
{
    public class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, AuthorDto[]>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorsHandler(IAuthorRepository authorRepository) 
        {
            _authorRepository = authorRepository;
        }

        public Task<AuthorDto[]> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var query = _authorRepository.GetQuery();

            if (request.AuthorIds != null && request.AuthorIds.Any())
            {
                query = query.Where(x => request.AuthorIds.Contains(x.Id));
            }

            if (!string.IsNullOrEmpty(request.AuthorFirstName))
            {
                query = query.Where(x => x.FirstName.Contains(request.AuthorFirstName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.AuthorLastName))
            {
                query = query.Where(x => x.LastName.Contains(request.AuthorLastName, StringComparison.InvariantCultureIgnoreCase));
            }

            var mapper = AuthorMapper.Mapper;

            var authors = query.Select(x => mapper.Map<AuthorDto>(x)).ToArray();
            return Task.FromResult(authors);
        }
    }
}
