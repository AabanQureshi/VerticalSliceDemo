using Application.Abstraction;
using Domain.ValueTypes;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using VerticalSliceDemo.Features.Product.Get;

namespace VerticalSliceDemo.Features.Product.GetById
{
    public sealed class GetProductByIdQuery : IQuery<GetProductResponse>
    {
        public required ProductId Id { get; set; }
    }

    public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductResponse>
    {
        private readonly ApplicationDBContext _context;

        public GetProductByIdQueryHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<GetProductResponse> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .Select(x => new GetProductResponse
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock
                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new KeyNotFoundException("Product Not Found");

            return product;
        }
    }
    public static class GetProductByIdEndpoint
    {
        public static IEndpointRouteBuilder MapGetProductByIdEndpoint(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/products/{id:guid}", async (
                Guid id,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                try
                {
                    var product = await dispatcher.DispatchQueryHandler<GetProductByIdQuery, GetProductResponse>(new GetProductByIdQuery { Id = id }, cancellationToken);
                    return Results.Ok(product);
                }
                catch(KeyNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            })
            .WithName("GetProductById")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Products");
            return routes;
        }
    }

}
