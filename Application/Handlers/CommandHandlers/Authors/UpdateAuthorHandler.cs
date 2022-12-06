using Application.Commands.Authors;
using Application.Exceptions;
using Application.Mappers.Authors;
using Application.Models.Authors;
using Core.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Authors
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FirstName))
            {
                throw new BadRequestException("Необходимо указать имя автора");
            }

            if (string.IsNullOrEmpty(request.LastName))
            {
                throw new BadRequestException("Необходимо указать фамилию автора");
            }

            var authorId = request.Id;

            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                throw new NotFoundException("Автор не найден");
            }

            var query = _authorRepository.GetQuery()
                .Where(x => x.Id != authorId)
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

            author.FirstName = request.FirstName;
            author.LastName = request.LastName;
            author.Birthdate = request.Birthdate;

            await _authorRepository.UpdateAsync(author);
            await _authorRepository.Commit();

            var mapper = AuthorMapper.Mapper;

            return mapper.Map<AuthorDto>(author);
        }
    }
}
