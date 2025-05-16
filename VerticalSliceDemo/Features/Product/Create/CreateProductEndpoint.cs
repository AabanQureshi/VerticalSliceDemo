using Application.Abstraction;
using Domain.ValueTypes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceDemo.Features.Product.Create
{
    public static class CreateProductEndpoint
    {
        public static IEndpointRouteBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/products", async (
                [FromBody] CreateProductDTO dto,
                IDispatcher dispatcher,
                CancellationToken cancellationToken,
                IValidator<CreateProductDTO> validator) =>
            {
                var validationResult = await validator.ValidateAsync(dto, cancellationToken);
                if (!validationResult.IsValid) return Results.ValidationProblem(validationResult.ToDictionary());

                var command = new CreateProductCommand
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Stock = dto.Stock
                };

                var productId = await dispatcher.DispatchCommandHandler<CreateProductCommand, ProductId>(command, cancellationToken);
                return Results.Created($"/products/{productId}", productId);
            })
            .WithName("CreateProduct")
            .Produces<ProductId>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Products");

            return routes;
        }

    }
}
