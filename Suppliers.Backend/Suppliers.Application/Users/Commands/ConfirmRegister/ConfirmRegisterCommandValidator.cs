using FluentValidation;


namespace Suppliers.Application.Users.Commands.ConfirmRegister
{
    public class ConfirmRegisterCommandValidator : AbstractValidator<ConfirmRegisterCommand>
    {
        public ConfirmRegisterCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
