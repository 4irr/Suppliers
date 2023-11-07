using MediatR;

namespace Suppliers.Application.Tenders.Commands.RegisterInTender
{
    public class RegisterInTenderCommand : IRequest
    {
        public Guid TenderId { get; set; }
        public Guid UserId { get; set; }
        public string? UserDescription { get; set; }
    }
}
