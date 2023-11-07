using MediatR;

namespace Suppliers.Application.Tenders.Queries.GetTenderDetails
{
    public class GetTenderDetailsQuery : IRequest<TenderDetailsVm>
    {
        public Guid TenderId { get; set; }
    }
}
