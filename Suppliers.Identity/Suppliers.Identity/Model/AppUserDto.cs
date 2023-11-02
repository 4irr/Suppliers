namespace Suppliers.Identity.Model
{
    public class AppUserDto
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string? Role { get; set; } = null!;
    }
}
