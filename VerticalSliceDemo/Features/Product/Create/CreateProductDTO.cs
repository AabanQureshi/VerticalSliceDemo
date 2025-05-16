using Domain.ValueTypes;

namespace VerticalSliceDemo.Features.Product.Create
{
    public sealed record CreateProductDTO
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required Price Price { get; init; }
        public required int Stock { get; init; }

    }
}
