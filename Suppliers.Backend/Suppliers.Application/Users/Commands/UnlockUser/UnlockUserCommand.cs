using MediatR;

namespace Suppliers.Application.Users.Commands.UnlockUser
{
    public class UnlockUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
