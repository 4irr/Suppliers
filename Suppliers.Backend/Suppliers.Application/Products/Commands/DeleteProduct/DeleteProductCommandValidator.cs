using FluentValidation;

namespace Suppliers.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(deleteProductCommand => deleteProductCommand.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
            RuleFor(deleteProductCommand => deleteProductCommand.UserId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
