using Application.Abstraction;

namespace VerticalSliceDemo.Features.Product.Get
{
    public static class GetProductsEndpoint
    {
        public static IEndpointRouteBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/products", async (
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                var products = await dispatcher.DispatchQueryHandler<GetProductsQuery, IEnumerable<GetProductResponse>>(new GetProductsQuery(), cancellationToken);
                return Results.Ok(products);
            })
            .WithName("GetProducts")
            .Produces<IEnumerable<GetProductResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Products");
            return routes;
        }
    }
}
