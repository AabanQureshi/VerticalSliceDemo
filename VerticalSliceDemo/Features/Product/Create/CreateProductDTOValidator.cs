using FluentValidation;

namespace VerticalSliceDemo.Features.Product.Create
{
    public sealed class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .Must(price => price.Amount > 0)
                .WithMessage("Price must be greater than 0.")
                .Must(price => !string.IsNullOrEmpty(price.Currency))
                .WithMessage("Currency is required.")
                .Must(price => price.Currency.Length <= 3)
                .WithMessage("Currency must be a 3-letter code.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0.");
        }
    }
}
