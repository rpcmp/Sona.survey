using Application.Responses.Books;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<BookDto>
    {
        [DefaultValue(false)]
        public int Id { get; set; }

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
