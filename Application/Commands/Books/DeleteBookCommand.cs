using MediatR;

namespace Application.Commands.Books
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; set; }
    }
}
