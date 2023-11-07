using MediatR;

namespace Suppliers.Application.Tenders.Queries.GetTendersList
{
    public class GetTenderListQuery : IRequest<TendersListVm>
    {
    }
}
