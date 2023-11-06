using FluentValidation;

namespace Suppliers.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            RuleFor(command => command.BatchId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.SupplierId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
