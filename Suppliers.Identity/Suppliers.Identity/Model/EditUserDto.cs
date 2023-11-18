namespace Suppliers.Identity.Model
{
    public class EditUserDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Organization { get; set; }
    }
}
