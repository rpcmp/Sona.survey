using MediatR;

namespace Application.Commands.Authors
{
    public class DeleteAuthorCommand : IRequest
    {
        public int Id { get; set; }
    }
}
