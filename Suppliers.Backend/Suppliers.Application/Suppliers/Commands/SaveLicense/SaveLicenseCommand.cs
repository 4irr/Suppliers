using MediatR;
using Microsoft.AspNetCore.Http;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class SaveLicenseCommand : IRequest
    {
        public Guid UserId { get; set; }
        public IFormFile? FormFile { get; set; }
        public string? FullPath { get; set; }
    }
}
