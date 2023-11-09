using FluentValidation;

namespace Suppliers.Application.Suppliers.Commands.LoadLicense
{
    public class LoadLicenseCommandValidator : AbstractValidator<LoadLicenseCommand>
    {
        public LoadLicenseCommandValidator() 
        {
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
