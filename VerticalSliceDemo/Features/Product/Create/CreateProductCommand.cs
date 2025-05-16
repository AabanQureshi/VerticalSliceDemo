using Application.Abstraction;
using Domain.ValueTypes;

namespace VerticalSliceDemo.Features.Product.Create
{
    public sealed class CreateProductCommand : ICommand<ProductId>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Price Price { get; set; }
        public required int Stock { get; set; }
    }
}
