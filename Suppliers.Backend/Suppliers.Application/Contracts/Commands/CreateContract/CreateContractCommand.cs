using MediatR;

namespace Suppliers.Application.Contracts.Commands.CreateContract
{
    public class CreateContractCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
    }
}
