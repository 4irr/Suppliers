using FluentValidation;

namespace Suppliers.Application.Tenders.Commands.RegisterInTender
{
    public class RegisterInTenderCommandValidator : AbstractValidator<RegisterInTenderCommand>
    {
        public RegisterInTenderCommandValidator() 
        {
            RuleFor(command => command.TenderId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.UserDescription)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .MaximumLength(500).WithMessage("Длина описания не должна превышать 500 символов");
        }
    }
}
