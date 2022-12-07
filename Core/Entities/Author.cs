using Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Author : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<Book> Books { get; set; }
    }
}
