namespace Domain.ValueTypes
{
    public sealed record Price(decimal Amount, string Currency)
    {
        public static implicit operator decimal(Price price) => price.Amount;
        public static implicit operator Price(decimal amount) => new(amount, "USD");
        public override string ToString() => $"{Amount} {Currency}";
    }
}
