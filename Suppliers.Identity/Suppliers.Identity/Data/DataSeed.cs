using Microsoft.AspNetCore.Identity;
using Suppliers.Identity.Model;

namespace Suppliers.Identity.Data
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(AuthDbContext context, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = "Client" },
                    new IdentityRole { Name = "Supplier" },
                    new IdentityRole { Name = "Admin" }
                };

                foreach (var role in roles)
                {
                    var result = await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = configuration.GetValue<string>("AdminEmail"),
                    EmailConfirmed = true
                };
                var client = new AppUser
                {
                    UserName = "client",
                    FirstName = "client",
                    LastName = "client",
                    Email = configuration.GetValue<string>("ClientEmail"),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "admin");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                result = await userManager.CreateAsync(client, "client");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(client, "Client");
                }
            }
        }
    }
}
