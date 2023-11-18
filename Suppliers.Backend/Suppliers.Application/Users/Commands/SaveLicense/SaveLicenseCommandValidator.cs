using FluentValidation;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class SaveLicenseCommandValidator : AbstractValidator<SaveLicenseCommand>
    {
        public SaveLicenseCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty).WithMessage("Не указан Id пользователя");
            RuleFor(command => command.FormFile)
                .NotNull().WithMessage("Файл не загружен");
            RuleFor(command => command.FullPath)
                .NotEmpty().WithMessage("Путь не должен быть пустым");
        }
    }
}
