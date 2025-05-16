using Domain.ValueTypes;

namespace Domain.Entities
{
    public sealed class Product
    {
        public ProductId Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Price Price { get; set; } = default!;
        public int Stock { get; set; } = default!;

    }
}
