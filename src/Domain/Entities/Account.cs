using System.Security.Cryptography;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Account(Iban id) : Entity<Iban>(id)
{
    public required Ulid OwnerId { get; set; }
    public required User Owner { get; init; }

    public required string Name { get; set; }
    public required Currency Currency { get; set; }
    public required Money Balance { get; set; }

    public static Account CreateNew(User owner, string name, Currency? currency = null)
    {
        var iban = Iban.Generate(RandomNumberGenerator.GetInt32(int.MaxValue), Convert.ToInt32(owner.Id.Time.Ticks));
        currency ??= owner.PreferredCurrency;

        return new Account(iban)
        {
            OwnerId = owner.Id,
            Owner = owner,
            Name = name,
            Currency = currency.Value,
            Balance = new Money(owner.PreferredCurrency, 0),
        };
    }
}