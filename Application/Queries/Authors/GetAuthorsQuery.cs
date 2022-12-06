using Application.Models.Authors;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAuthorsQuery : IRequest<AuthorDto[]>
    {
        public int[] AuthorIds { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
