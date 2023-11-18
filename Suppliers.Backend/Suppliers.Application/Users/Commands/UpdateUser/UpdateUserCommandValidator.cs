using FluentValidation;

namespace Suppliers.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.FirstName)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.LastName)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.Age)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .GreaterThan(0).WithMessage("Недопустимый возраст")
                .LessThan(120).WithMessage("Недопустимый возраст");
        }
    }
}
