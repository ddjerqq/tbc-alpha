namespace Domain.ValueObjects;

public readonly record struct Money(Currency Currency, decimal Amount)
{
    public decimal Amount { get; private init; } = Amount < 0
        ? throw new ArgumentException("Amount cannot be negative")
        : Amount;

    public Money Add(Money money) => this with { Amount = Amount + money.Amount };

    public Money Subtract(Money money) => this with { Amount = Amount - money.Amount };

    public static Money Parse(string value)
    {
        var parts = value.Split('-');

        return parts switch
        {
            [var currency, var amount] => new Money(new Currency(currency), decimal.Parse(parts[1])),
            _ => throw new FormatException("Invalid money format."),
        };
    }

    public override string ToString() => $"{Currency}-{Amount}";
}