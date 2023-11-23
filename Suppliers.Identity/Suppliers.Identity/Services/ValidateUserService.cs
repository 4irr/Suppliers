using Microsoft.AspNetCore.Identity;
using Suppliers.Identity.Model;

namespace Suppliers.Identity.Services
{
    public class ValidateUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public ValidateUserService(UserManager<AppUser> userManager) => _userManager = userManager;

        public async Task<ValidationResult> ValidateAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            ValidationResult result = new ValidationResult { Succeeded = true };

            if (user == null)
            {
                result.Succeeded = false;
                result.Message = "Неверный логин или пароль";

                return result;
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                result.Succeeded = false;
                result.Message = "Вы не подтвердили свой email";
            }

            if (!user!.IsRegisterConfirmed)
            {
                result.Succeeded = false;
                result.Message = "Дождитесь подтверждения регистрации администратором";
            }

            if (!user.IsEnabled)
            {
                result.Succeeded = false;
                result.Message = "Ваш аккаунт был заблокирован";
            }

            return result;
        }
    }
}
