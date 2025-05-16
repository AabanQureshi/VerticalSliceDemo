using Application.Abstraction;
using Domain.ValueTypes;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VerticalSliceDemo.Features.Product.Remove
{
    public sealed class RemoveProductCommand : ICommand<RemoveProductCommandResponse>
    {
        public required ProductId Id { get; set; }
    }

    public sealed record RemoveProductCommandResponse(bool Success);


    public sealed class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand, RemoveProductCommandResponse>
    {
        private readonly ApplicationDBContext _context;

        public RemoveProductCommandHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<RemoveProductCommandResponse> HandleAsync(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException("Product Not Found");

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return new RemoveProductCommandResponse(result > 0);
        }
    }

    public static class RemoveProductEndpoint
    {
        public static IEndpointRouteBuilder MapRemoveProductEndpoint(this IEndpointRouteBuilder builder)
        {
            builder.MapDelete("/products/{id:guid}", async (  
                Guid id, 
                CancellationToken cancellationToken,
                IDispatcher dispatcher) =>
            {
                var command = new RemoveProductCommand { Id = id };
                var result = await dispatcher.DispatchCommandHandler<RemoveProductCommand, RemoveProductCommandResponse>(command, cancellationToken);
                return result.Success ? Results.NoContent() : Results.NotFound();
            })
            .WithName("RemoveProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Products");
            return builder;
        }
    }
}
