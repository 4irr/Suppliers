using FluentValidation;

namespace Suppliers.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.OldPassword)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.NewPassword)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
