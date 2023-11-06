using MediatR;

namespace Suppliers.Application.Contracts.Commands.ConfirmContract
{
    public class ConfirmContractCommand : IRequest
    {
        public Guid ContractId { get; set; }
    }
}
