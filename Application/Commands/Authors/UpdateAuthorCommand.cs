using Application.Models.Authors;
using MediatR;
using System;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest<AuthorDto>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
