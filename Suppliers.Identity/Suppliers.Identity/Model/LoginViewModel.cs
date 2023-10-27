using System.ComponentModel.DataAnnotations;

namespace Suppliers.Identity.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
