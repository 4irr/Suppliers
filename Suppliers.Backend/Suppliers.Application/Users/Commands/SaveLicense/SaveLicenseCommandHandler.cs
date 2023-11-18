using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class SaveLicenseCommandHandler : IRequestHandler<SaveLicenseCommand>
    {
        private readonly IUsersHttpClient _apiClient;

        public SaveLicenseCommandHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task Handle(SaveLicenseCommand request, CancellationToken cancellationToken)
        {
            using (var fileStream = new FileStream(request.FullPath!, FileMode.Create))
            {
                await request.FormFile!.CopyToAsync(fileStream);
            }

            var content = new LicenseDto
            {
                UserId = request.UserId.ToString(),
                FullPath = request.FullPath
            };

            await _apiClient.SaveUserLicenseAsync(content);
        }
    }
}
