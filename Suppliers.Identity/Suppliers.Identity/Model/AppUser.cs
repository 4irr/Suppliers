using Microsoft.AspNetCore.Identity;

namespace Suppliers.Identity.Model
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string? Organization { get; set; }
        public bool IsLicenseLoaded { get; set; }
        public bool IsLicensed { get; set; }
        public string? LicensePath { get; set; }
        public bool IsRegisterConfirmed { get; set; }
        public bool IsEnabled { get; set; }
    }
}
