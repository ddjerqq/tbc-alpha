using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Account(Iban id) : Entity<Iban>(id)
{
    public required Ulid OwnerId { get; init; }
    public required User Owner { get; init; }

    public required string Name { get; init; }
    public required Currency Currency { get; init; }

    public Money Balance => Transactions
        .Aggregate(new Money(Currency, 0), (acc, x) => x.IsSender
            ? acc.Subtract(x.Transaction.Amount)
            : acc.Add(x.Transaction.Amount));

    public required List<TransactionParticipant> Transactions { get; init; } = [];
}