using FluentValidation;

namespace Suppliers.Application.Tenders.Commands.CloseTender
{
    public class CloseTenderCommandValidator : AbstractValidator<CloseTenderCommand>
    {
        public CloseTenderCommandValidator() 
        {
            RuleFor(command => command.TenderId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.ExecutorId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
