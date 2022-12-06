using Application.Responses.Books;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Books
{
    public class CreateBookCommand : IRequest<BookDto>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Неоходимо указать название книги")]
        public string Title { get; set; }

        public uint Year { get; set; }

        [DefaultValue(false)]
        [Required(ErrorMessage = "Необходимо указать автора книги")]
        public int AuthorId { get; set; }

        [DefaultValue(false)]
        [Required(ErrorMessage = "Необходимо указать жанр книги")]
        public int GenreId { get; set; }
    }
}
