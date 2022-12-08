using Application.Models.Authors;
using Application.Models.Genres;

namespace Application.Responses.Books
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public uint Year { get; set; }

        public GenreDto Genre { get; set; }

        public AuthorDto Author { get; set; }

    }
}
