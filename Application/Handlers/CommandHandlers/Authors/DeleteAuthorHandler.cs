using Application.Commands.Authors;
using Application.Exceptions;
using Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Authors
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorId = request.Id;

            var author = _authorRepository.GetQuery()
                .Where(x => x.Id == authorId)
                .Include(x => x.Books)
                .FirstOrDefault();

            if (author == null)
            {
                throw new NotFoundException("Автор не найден");
            }

            if (author.Books.Any())
            {
                throw new ConflictException("У данного автора есть книги. Перед удалением автора необходимо удалить книги этого автора");
            }

            await _authorRepository.DeleteAsync(author);
            await _authorRepository.Commit();

            return Unit.Value;
        }
    }
}
