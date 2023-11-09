using Suppliers.Application.Suppliers.Commands.LoadLicense;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;

namespace Suppliers.Application.Interfaces
{
    public interface IUsersHttpClient
    {
        Task<List<AppUserDto>> GetAllUsersAsync();

        Task<AppUserDto?> GetUserByIdAsync(Guid id);

        Task SaveUserLicenseAsync(LicenseDto content);

        Task<string?> LoadUserLicenseAsync(Guid id);

        Task ConfirmUserLicense(Guid id);
    }
}
