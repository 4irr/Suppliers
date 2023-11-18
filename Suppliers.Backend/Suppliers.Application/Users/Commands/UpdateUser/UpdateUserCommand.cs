using MediatR;

namespace Suppliers.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Organization { get; set; }
    }
}
