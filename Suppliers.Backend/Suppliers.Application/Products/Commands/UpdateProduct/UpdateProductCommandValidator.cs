using FluentValidation;

namespace Suppliers.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(updateProductCommand => updateProductCommand.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
            RuleFor(updateProductCommand => updateProductCommand.Name)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .MaximumLength(250).WithMessage("Количество символов не должно превышать 250");
            RuleFor(updateProductCommand => updateProductCommand.Price)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .GreaterThan(0).WithMessage("Цена должна быть неотрицательной");
            RuleFor(updateProductCommand => updateProductCommand.Quantity)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .GreaterThan(0).WithMessage("Количество должно быть неотрицательным");
        }
    }
}
