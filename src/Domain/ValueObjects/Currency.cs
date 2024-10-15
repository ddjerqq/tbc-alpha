namespace Domain.ValueObjects;

public readonly record struct Currency(string Value) : IValueObject
{
    public string Value { get; init; } = string.IsNullOrWhiteSpace(Value) || Value.Length != 3 || !Value.All(char.IsLetter) || Value.Any(char.IsUpper)
        ? throw new FormatException("Currency code must be a 3-letter uppercase string")
        : Value;

    public override string ToString() => Value;
}