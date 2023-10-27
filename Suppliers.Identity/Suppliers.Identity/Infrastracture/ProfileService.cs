using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Suppliers.Identity.Model;
using System.Security.Claims;

namespace Suppliers.Identity.Infrastracture
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            ClaimsPrincipal subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            string? subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault()?.Value;

            var user = await userManager.FindByIdAsync(subjectId!);

            context.AddRequestedClaims(context.Subject.Claims);
            if (context.Caller != "ClaimsProviderAccessToken")
            {
                var roles = await userManager.GetRolesAsync(user!);
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, user!.FirstName!),
                    new Claim(JwtClaimTypes.FamilyName, user!.LastName!),
                    new Claim(JwtClaimTypes.Role, roles.FirstOrDefault()!)
                };
                context.IssuedClaims.AddRange(claims);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = userManager.GetUserAsync(context.Subject).Result;

            context.IsActive = (user != null);
            return Task.CompletedTask;
        }
    }
}
