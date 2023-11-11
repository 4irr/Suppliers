using FluentValidation;

namespace Suppliers.Application.Reports.Commands.CreateTotalReport
{
    public class CreateTotalReportCommandValidator : AbstractValidator<CreateTotalReportCommand>
    {
        public CreateTotalReportCommandValidator() 
        {
            RuleFor(command => command.Beginning)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.Ending)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .Must((command, value) => value >= command.Beginning).WithMessage("Дата введена некорректно");
        }
    }
}
