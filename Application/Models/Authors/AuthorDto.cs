using System;

namespace Application.Models.Authors
{
    public class AuthorDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
