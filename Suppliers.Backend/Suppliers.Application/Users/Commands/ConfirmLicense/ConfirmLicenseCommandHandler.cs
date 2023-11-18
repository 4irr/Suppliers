using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Suppliers.Commands.ConfirmLicense
{
    public class ConfirmLicenseCommandHandler : IRequestHandler<ConfirmLicenseCommand>
    {
        private readonly IUsersHttpClient _apiClient;

        public ConfirmLicenseCommandHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task Handle(ConfirmLicenseCommand request, CancellationToken cancellationToken)
        {
            await _apiClient.ConfirmUserLicense(request.UserId);
        }
    }
}
