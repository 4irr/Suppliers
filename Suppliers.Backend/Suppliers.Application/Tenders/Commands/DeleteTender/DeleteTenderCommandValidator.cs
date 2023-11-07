using FluentValidation;

namespace Suppliers.Application.Tenders.Commands.DeleteTender
{
    public class DeleteTenderCommandValidator : AbstractValidator<DeleteTenderCommand>
    {
        public DeleteTenderCommandValidator() 
        {
            RuleFor(command => command.TenderId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}