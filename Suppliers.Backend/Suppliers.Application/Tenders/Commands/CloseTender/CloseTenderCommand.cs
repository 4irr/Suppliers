using MediatR;

namespace Suppliers.Application.Tenders.Commands.CloseTender
{
    public class CloseTenderCommand : IRequest
    {
        public Guid TenderId { get; set; }
        public Guid ExecutorId { get; set; }
    }
}
