using Application.Abstraction;
using Domain.ValueTypes;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VerticalSliceDemo.Features.Product.Update
{
    public sealed class UpdateProductCommand : ICommand<ProductId>
    {
        public required ProductId Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Price Price { get; set; }
        public required int Stock { get; set; }
    }

    public sealed record UpdateProductDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Price Price { get; set; }
        public required int Stock { get; set; }
    }

    public sealed class UpdateProductDTOValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidator()
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
    public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ProductId>
    {
        private readonly ApplicationDBContext _context;
        public UpdateProductCommandHandler(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ProductId> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == command.Id).FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException("Product not found");

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Stock = command.Stock;
            await _context.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
    public static class UpdateProductEndpoint
    {
        public static IEndpointRouteBuilder MapUpdateProductEndpoint(this IEndpointRouteBuilder routes)
        {
            routes.MapPut("/products/{id:guid}", async (
                Guid id,
                UpdateProductDTO productDTO,
                IDispatcher dispatcher,
                CancellationToken cancellationToken,
                IValidator<UpdateProductDTO> validator) =>
            {
                var validationResult = await validator.ValidateAsync(productDTO, cancellationToken);
                if (!validationResult.IsValid) return Results.ValidationProblem(validationResult.ToDictionary());

                var command = new UpdateProductCommand
                {
                    Id = id,
                    Name = productDTO.Name,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    Stock = productDTO.Stock
                };
                try
                {
                    var result = await dispatcher.DispatchCommandHandler<UpdateProductCommand, ProductId>(command, cancellationToken);
                    return Results.Ok(result);
                }
                catch (KeyNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }

            })
            .WithName("UpdateProductCommand")
            .Produces<ProductId>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Products");
            return routes;
        }
    }
}
