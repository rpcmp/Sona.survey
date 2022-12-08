using Application.Models.Authors;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Authors
{
    public class CreateAuthorCommand : IRequest<AuthorDto>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
