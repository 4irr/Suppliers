using MediatR;

namespace Suppliers.Application.Contracts.Commands.DeleteContract
{
    public class DeleteContractCommand : IRequest
    {
        public Guid ContractId { get; set; }
    }
}
