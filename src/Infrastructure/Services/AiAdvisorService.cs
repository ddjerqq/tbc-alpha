using Domain.Aggregates;
using Domain.ValueObjects;
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using grpc = Infrastructure.GetFinancialAdviceForAccountRequest.Types;

namespace Infrastructure.Services;

public sealed class AiAdvisorService
{
    private readonly AiAdvisor.AiAdvisorClient _client;

    public AiAdvisorService()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5001");
        _client = new AiAdvisor.AiAdvisorClient(channel);
    }

    public async Task<string> GetFinancialAdvice(User user, Iban accountId, CancellationToken ct = default)
    {
        var account = user.Accounts.First(account => account.Id == accountId);

        var request = new GetFinancialAdviceForAccountRequest
        {
            Balance = (double)account.Balance.Amount,
            Transactions =
            {
                account.Transactions.Select(tx => new grpc.Transaction
                {
                    Amount = (double)tx.Transaction.Amount.Amount,
                    Category = tx.Transaction.TransactionCategory.ToString(),
                }).ToList(),
            },
            Goals =
            {
                user.SavingGoals.Select(goal => new grpc.Goal
                {
                    Category = goal.Type,
                    AmountSaved = (double)goal.AmountSaved.Amount,
                    Total = (double)goal.Total.Amount,
                }).ToList(),
            },
        };

        var response = await _client.GetFinancialAdviceAsync(request, cancellationToken: ct);
        return response.Message;
    }
}