using Core.Entities.Base;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Genre : Entity
    {
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
