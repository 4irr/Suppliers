using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class LoadLicenseDto
    {
        public MemoryStream? FileStream { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
    }
}
