namespace Domain.ValueTypes
{
    public sealed record ProductId(Guid Id)
    {
        public static implicit operator Guid(ProductId productId) => productId.Id;
        public static implicit operator ProductId(Guid id) => new(id);
        public override string ToString() => Id.ToString();
    }
}
