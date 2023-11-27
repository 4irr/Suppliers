namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class LoadLicenseDto
    {
        public MemoryStream? FileStream { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
}
