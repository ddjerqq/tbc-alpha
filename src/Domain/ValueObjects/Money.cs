namespace Domain.ValueObjects;

public readonly record struct Money(Currency Currency, decimal Amount)
{
    public decimal Amount { get; private init; } = Amount < 0
        ? throw new ArgumentException("Amount cannot be negative")
        : Amount;

    public Money Add(Money money) => this with { Amount = Amount + money.Amount };

    public Money Subtract(Money money) => this with { Amount = Amount - money.Amount };

    public override string ToString() => $"{Amount} {Currency}";
}