using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suppliers.Identity.Model;
using static IdentityServer4.IdentityServerConstants;

namespace Suppliers.Identity.Controllers
{
    [Route("localApi")]
    [Authorize(LocalApi.PolicyName)]
    public class LocalApiController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public LocalApiController(UserManager<AppUser> userManager) => _userManager = userManager;

        [HttpGet("users")]
        public async Task<ActionResult> GetUserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<AppUserDto>();

            foreach(var user in users)
            {
                result.Add(new AppUserDto   
                {
                    Id = user.Id,
                    FirstName= user.FirstName,
                    LastName= user.LastName,
                    Age= user.Age,
                    Email = user.Email!,
                    Role = (await _userManager.GetRolesAsync(user)).First(),
                    Organization = user.Organization,
                    IsLicenseLoaded = user.IsLicenseLoaded,
                    IsLicensed = user.IsLicensed,
                    LicensePath = user.LicensePath,
                    EmailConfirmed = user.EmailConfirmed,
                    IsRegisterConfirmed = user.IsRegisterConfirmed,
                    IsEnabled = user.IsEnabled
                });
            }
            return Ok(result);
        }

        [HttpPost("users/save-license")]
        public async Task<ActionResult> SaveUserLicense([FromBody] LicenseDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId!);
            if (user == null)
            {
                return NotFound();
            }
            user.IsLicenseLoaded = true;
            user.LicensePath = dto.FullPath;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpGet("users/{id}/load-license")]
        public async Task<ActionResult> LoadUserLicense(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.LicensePath);
        }

        [HttpPost("users/{id}/confirm-license")]
        public async Task<ActionResult> ConfirmUserLicense(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsLicensed = true;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpPut("users")]
        public async Task<ActionResult> EditUser([FromBody] EditUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id!);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = dto.FirstName!;
            user.LastName = dto.LastName!;
            user.Age = dto.Age;
            user.Organization = dto.Organization;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpPut("users/password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id!);

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, dto.OldPassword!, dto.NewPassword!);

            if (!result.Succeeded)
            {
                if(result.Errors.Any(e => e.Code == "PasswordMismatch"))
                    return BadRequest("Старый пароль введён неверно");
                return BadRequest("Не удалось изменить пароль (пароль должен содержать цифры и символы латинского алфавита)");
            }

            return Ok();
        }

        [HttpPut("users/{id}/register/confirm")]
        public async Task<ActionResult> ConfirmRegister(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            user.IsRegisterConfirmed = true;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPut("users/{id}/block")]
        public async Task<ActionResult> BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsEnabled = false;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPut("users/{id}/unlock")]
        public async Task<ActionResult> UnlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsEnabled = true;
            await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}
