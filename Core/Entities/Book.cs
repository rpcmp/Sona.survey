using Core.Entities.Base;

namespace Core.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }

        public uint Year { get; set; }

        public Genre Genre { get; set; }

        public Author Author { get; set; }
    }
}
