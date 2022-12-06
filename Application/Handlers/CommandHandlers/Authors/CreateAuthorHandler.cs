using Application.Commands.Authors;
using Application.Exceptions;
using Application.Mappers.Authors;
using Application.Models.Authors;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Authors
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorHandler(IAuthorRepository authorRepository) 
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FirstName))
            {
                throw new BadRequestException("Необходимо указать имя автора");
            }

            if (string.IsNullOrEmpty(request.LastName))
            {
                throw new BadRequestException("Необходимо указать фамилию автора");
            }

            var query = _authorRepository.GetQuery()
                .Where(x => x.FirstName.Equals(request.FirstName, StringComparison.InvariantCultureIgnoreCase))
                .Where(x => x.LastName.Equals(request.LastName, StringComparison.InvariantCultureIgnoreCase));

            if (request.Birthdate.HasValue)
            {
                query = query.Where(x => x.Birthdate == request.Birthdate);
            }

            if (query.Any())
            {
                throw new ConflictException("Такой автор уже существует");
            }

            var author = new Author()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Birthdate = request.Birthdate
            };

            await _authorRepository.AddAsync(author);
            await _authorRepository.Commit();

            var mapper = AuthorMapper.Mapper;

            return mapper.Map<AuthorDto>(author);
        }
    }
}
