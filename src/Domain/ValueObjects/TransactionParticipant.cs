using Domain.Entities;

namespace Domain.ValueObjects;

public sealed class TransactionParticipant : IValueObject
{
    public required Ulid TransactionId { get; set; }
    public required Transaction Transaction { get; set; }

    public required Iban AccountId { get; set; }
    public required Account Account { get; set; }

    public required bool IsSender { get; set; }
}