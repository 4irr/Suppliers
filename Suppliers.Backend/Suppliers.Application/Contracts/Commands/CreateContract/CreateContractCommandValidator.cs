using FluentValidation;

namespace Suppliers.Application.Contracts.Commands.CreateContract
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator() 
        {
            RuleFor(command => command.OrderId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
