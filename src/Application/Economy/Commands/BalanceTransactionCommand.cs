using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Economy.Commands;

public sealed record BalanceTransactionCommand(Account Sender, Account Receiver, Money Amount) : IRequest<bool>;

public sealed class BalanceTransactionCommandHandler(IAppDbContext dbContext, ICurrencyExchange currencyExchange)
    : IRequestHandler<BalanceTransactionCommand, bool>
{
    public async Task<bool> Handle(BalanceTransactionCommand request, CancellationToken ct)
    {
        Money senderBalance = await currencyExchange.ConvertToAsync(request.Sender.Balance, request.Receiver.Currency);
        Money SenderAmount = await currencyExchange.ConvertToAsync(request.Amount, request.Receiver.Currency);


        if (senderBalance.Amount < SenderAmount.Amount) return false;


        var transaction = new Transaction(Ulid.NewUlid())
        {
            Amount = new Money(request.Receiver.Currency, SenderAmount.Amount),
            Date = DateTime.UtcNow,
            TransactionCategory = TransactionCategory.Transfers,
            Participants = [],
        };

        transaction.Participants.Add(new TransactionParticipant(transaction, request.Sender, true));
        transaction.Participants.Add(new TransactionParticipant(transaction, request.Receiver, false));


        await dbContext.SaveChangesAsync(ct);
        return true;
    }
}