using Domain.ValueTypes;

namespace VerticalSliceDemo.Features.Product.Get
{
    public sealed record GetProductResponse
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Price Price { get; set; }
        public required int Stock { get; set; }

    }
}
