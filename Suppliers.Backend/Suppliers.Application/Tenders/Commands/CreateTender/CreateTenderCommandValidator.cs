using FluentValidation;

namespace Suppliers.Application.Tenders.Commands.CreateTender
{
    public class CreateTenderCommandValidator : AbstractValidator<CreateTenderCommand>
    {
        public CreateTenderCommandValidator() 
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .MaximumLength(50).WithMessage("Длина заголовка не должна превышать 50 символов");
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .MaximumLength(500).WithMessage("Длина описания не должна превышать 500 символов");
            RuleFor(command => command.Beginning)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.Ending)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .Must(IsDateValid);
        }

        private bool IsDateValid(CreateTenderCommand command, DateTime ending)
        {
            return (command.Beginning < ending) ? true : false;
        }
    }
}
