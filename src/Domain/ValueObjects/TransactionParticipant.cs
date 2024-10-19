using Domain.Entities;

namespace Domain.ValueObjects;

public sealed class TransactionParticipant() : IValueObject
{
    public Ulid TransactionId { get; init; }
    public Transaction Transaction { get; init; } = default!;

    public Iban AccountId { get; init; }
    public Account Account { get; init; } = default!;

    public bool IsSender { get; init; }

    public TransactionParticipant(Transaction transaction, Account account, bool isSender) : this()
    {
        TransactionId = transaction.Id;
        Transaction = transaction;
        AccountId = account.Id;
        Account = account;
        IsSender = isSender;
    }
}