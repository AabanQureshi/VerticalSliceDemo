using Application.Abstraction;
using Domain.ValueTypes;
using Infrastructure.Data;

namespace VerticalSliceDemo.Features.Product.Create
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductId>
    {
        private readonly ApplicationDBContext _Context;

        public CreateProductCommandHandler(ApplicationDBContext context)
        {
            _Context = context;
        }

        public async Task<ProductId> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Stock = command.Stock
            };

            await _Context.Products.AddAsync(product, cancellationToken);

            await _Context.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
