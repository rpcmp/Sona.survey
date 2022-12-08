using Application.Commands.Books;
using Application.Exceptions;
using Application.Mappers.Books;
using Application.Responses.Books;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Books
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public CreateBookHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                throw new BadRequestException("Необходимо указать название книги");
            }

            var author = await _authorRepository.GetByIdAsync(request.Author.Id);
            if (author == null)
            {
                throw new NotFoundException("Автор книги не найден");
            }

            var genre = await _genreRepository.GetByIdAsync(request.Genre.Id);
            if (genre == null)
            {
                throw new NotFoundException("Жанр книги не найден");
            }

            var book = new Book()
            {
                Title = request.Title,
                Year = request.Year,
                Author = author,
                Genre = genre
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.Commit();

            var mapper = BookMapper.Mapper;

            return mapper.Map<BookDto>(book);
        }
    }
}
