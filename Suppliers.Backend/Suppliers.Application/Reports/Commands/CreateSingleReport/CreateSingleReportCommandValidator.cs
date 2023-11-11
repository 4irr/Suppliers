using FluentValidation;

namespace Suppliers.Application.Reports.Commands.CreateReport
{
    public class CreateSingleReportCommandValidator : AbstractValidator<CreateSingleReportCommand>
    {
        public CreateSingleReportCommandValidator() 
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
