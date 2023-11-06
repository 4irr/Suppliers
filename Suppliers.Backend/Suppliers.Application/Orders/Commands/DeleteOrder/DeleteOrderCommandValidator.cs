using FluentValidation;

namespace Suppliers.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator() 
        {
            RuleFor(command => command.OrderId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
