using FluentValidation;

namespace Suppliers.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(createProductCommand => createProductCommand.Name)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .MaximumLength(250).WithMessage("Количество символов не должно превышать 250");
            RuleFor(createProductCommand => createProductCommand.Price)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .GreaterThan(0).WithMessage("Цена должна быть неотрицательной");
            RuleFor(createProductCommand => createProductCommand.Quantity)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .GreaterThan(0).WithMessage("Количество должно быть неотрицательным");
        }
    }
}
