using MediatR;

namespace Suppliers.Application.Users.Commands.BlockUser
{
    public class BlockUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
