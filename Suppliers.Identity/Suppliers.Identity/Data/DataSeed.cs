using Microsoft.AspNetCore.Identity;
using Suppliers.Identity.Model;

namespace Suppliers.Identity.Data
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(AuthDbContext context, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
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
                var user = new AppUser
                {
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Age = 20,
                    Email = "chirva2015@list.ru"
                };

                var result = await userManager.CreateAsync(user, "admin");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
