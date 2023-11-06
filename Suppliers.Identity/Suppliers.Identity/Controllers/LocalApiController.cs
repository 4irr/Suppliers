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
                });
            }
            return Ok(result);
        }
    }
}
