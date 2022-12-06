using Application.Commands.Books;
using Application.Exceptions;
using Application.Mappers.Books;
using Application.Responses.Books;
using Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Books
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public UpdateBookHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new NotFoundException("Книга не найдена");
            }

            if (string.IsNullOrEmpty(request.Title))
            {
                throw new BadRequestException("Необходимо указать название книги");
            }

            var author = await _authorRepository.GetByIdAsync(request.AuthorId);
            if (author == null)
            {
                throw new NotFoundException("Автор книги не найден");
            }

            var genre = await _genreRepository.GetByIdAsync(request.GenreId);
            if (genre == null)
            {
                throw new NotFoundException("Жанр книги не найден");
            }

            book.Title = request.Title;
            book.Year = request.Year;
            book.Author = author;
            book.Genre = genre;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.Commit();

            var mapper = BookMapper.Mapper;
            return mapper.Map<BookDto>(book);
        }
    }
}
