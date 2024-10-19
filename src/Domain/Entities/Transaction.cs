using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Transaction(Ulid id) : Entity<Ulid>(id)
{
    public required Money Amount { get; init; }
    public required DateTime Date { get; init; }
    public required TransactionCategory TransactionCategory { get; init; }

    public required ICollection<TransactionParticipant> Participants { get; init; } = [];
}