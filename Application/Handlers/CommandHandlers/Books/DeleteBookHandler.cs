using Application.Commands.Books;
using Application.Exceptions;
using Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Books
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookId = request.Id;

            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new NotFoundException("Книга не найдена");
            }

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.Commit();

            return Unit.Value;
        }
    }
}
