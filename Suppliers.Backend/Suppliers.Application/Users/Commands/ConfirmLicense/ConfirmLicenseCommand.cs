using MediatR;

namespace Suppliers.Application.Suppliers.Commands.ConfirmLicense
{
    public class ConfirmLicenseCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
