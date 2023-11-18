using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class LoadLicenseCommandHandler : IRequestHandler<LoadLicenseCommand, LoadLicenseDto>
    {
        private readonly IUsersHttpClient _apiClient;

        public LoadLicenseCommandHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task<LoadLicenseDto> Handle(LoadLicenseCommand request, CancellationToken cancellationToken)
        {
            var filePath = await _apiClient.LoadUserLicenseAsync(request.UserId);

            if(filePath == null)
            {
                throw new Exception("Failed to load user license");
            }

            var memoryStream = new MemoryStream();
            var fileInfo = new FileInfo(filePath);

            if(!fileInfo.Exists)
            {
                throw new Exception("Failed to load user license");
            }

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;

            string contentType;

            new FileExtensionContentTypeProvider().TryGetContentType(fileInfo.Name, out contentType);

            return new LoadLicenseDto
            {
                FileStream = memoryStream,
                ContentType = contentType,
                FileName = fileInfo.Name
            };
        }
    }
}
