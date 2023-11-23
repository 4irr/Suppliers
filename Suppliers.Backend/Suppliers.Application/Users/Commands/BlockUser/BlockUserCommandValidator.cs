using FluentValidation;

namespace Suppliers.Application.Users.Commands.BlockUser
{
    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
