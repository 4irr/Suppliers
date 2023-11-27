using MediatR;

namespace Suppliers.Application.Contracts.Queries.GetContractsList
{
    public class GetContractsListQuery : IRequest<ContractListVm>
    {
    }
}
