using MediatR;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class LoadLicenseCommand : IRequest<LoadLicenseDto>
    {
        public Guid UserId { get; set; }
    }
}
