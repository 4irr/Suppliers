using System.ComponentModel.DataAnnotations;

namespace Suppliers.Identity.Model
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Неверный формат")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 120, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(5, ErrorMessage = "Поле должно содержать минимум 5 символов")]
        public string Organization { get; set; } = null!;

        public string ReturnUrl { get; set; } = null!;
    }
}
