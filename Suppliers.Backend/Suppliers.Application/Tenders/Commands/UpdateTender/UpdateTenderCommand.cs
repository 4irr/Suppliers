using MediatR;

namespace Suppliers.Application.Tenders.Commands.UpdateTender
{
    public class UpdateTenderCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
    }
}
