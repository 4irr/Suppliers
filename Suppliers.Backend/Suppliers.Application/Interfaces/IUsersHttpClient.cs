using Suppliers.Application.Suppliers.Commands.LoadLicense;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using Suppliers.Application.Users.Commands.ChangePassword;
using Suppliers.Application.Users.Commands.UpdateUser;

namespace Suppliers.Application.Interfaces
{
    public interface IUsersHttpClient
    {
        Task<List<AppUserDto>> GetAllUsersAsync();

        Task<AppUserDto?> GetUserByIdAsync(Guid id);

        Task SaveUserLicenseAsync(LicenseDto content);

        Task<string?> LoadUserLicenseAsync(Guid id);

        Task ConfirmUserLicense(Guid id);

        Task UpdateUser(EditUserDto dto);

        Task ChangePassword(ChangePasswordDto dto);
    }
}
