using MediatR;

namespace Suppliers.Application.Users.Commands.ConfirmRegister
{
    public class ConfirmRegisterCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
