using Application.Commands.Authors;
using Application.Exceptions;
using Core.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Authors
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public DeleteAuthorHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorId = request.Id;

            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                throw new NotFoundException("Автор не найден");
            }

            var booksQuery = _bookRepository.GetQuery().Where(x => x.Author.Id == authorId);
            if (booksQuery.Any())
            {
                throw new ConflictException("У данного автора есть книги. Перед удалением автора необходимо удалить книги этого автора");
            }

            await _authorRepository.DeleteAsync(author);
            await _authorRepository.Commit();

            return Unit.Value;
        }
    }
}
