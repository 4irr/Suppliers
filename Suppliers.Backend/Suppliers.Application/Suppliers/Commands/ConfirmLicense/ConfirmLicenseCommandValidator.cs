using FluentValidation;

namespace Suppliers.Application.Suppliers.Commands.ConfirmLicense
{
    public class ConfirmLicenseCommandValidator : AbstractValidator<ConfirmLicenseCommand>
    {
        public ConfirmLicenseCommandValidator() 
        {
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
