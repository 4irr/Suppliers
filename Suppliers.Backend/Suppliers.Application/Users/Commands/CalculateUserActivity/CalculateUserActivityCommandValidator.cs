using FluentValidation;

namespace Suppliers.Application.Users.Commands.CalculateUserActivity
{
    public class CalculateUserActivityCommandValidator : AbstractValidator<CalculateUserActivityCommand>
    {
        public CalculateUserActivityCommandValidator() 
        {
            RuleFor(command => command.SupplierId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.Beginning)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.Ending)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .Must((command, value) => value >= command.Beginning).WithMessage("Дата введена некорректно");
        }
    }
}
