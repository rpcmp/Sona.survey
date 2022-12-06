using Application.Models.Authors;
using MediatR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest<AuthorDto>
    {
        [DefaultValue(false)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо указать имя автора")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо указать фамилию автора")]
        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
