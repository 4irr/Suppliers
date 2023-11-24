using MediatR;

namespace Suppliers.Application.Users.Commands.CalculateUserActivity
{
    public class CalculateUserActivityCommand : IRequest<UserActivityVm>
    {
        public Guid SupplierId { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
    }
}
