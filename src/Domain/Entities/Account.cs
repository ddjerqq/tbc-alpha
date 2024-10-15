using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Account : Entity<Iban>
{
    public Ulid OwnerId { get; set; }

    public User Owner { get; init; }

    public Currency Currency => Balance.Currency;

    public Money Balance { get; set; }

    // for ef core
    private Account(Iban iban, Ulid ownerId, User user, Money balance) : base(iban)
    {
        OwnerId = ownerId;
        Owner = user;
        Balance = balance;
    }

    public Account(User owner) : this(
        Iban.Generate(Constants.BankName, Constants.BankCountryCode, Convert.ToInt32(owner.Id.Random)),
        owner.Id,
        owner,
        new Money(owner.PreferredCurrency, 0))
    {
    }
}