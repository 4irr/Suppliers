using MediatR;

namespace Suppliers.Application.Tenders.Commands.DeleteTender
{
    public class DeleteTenderCommand : IRequest
    {
        public Guid TenderId { get; set; }
    }
}
