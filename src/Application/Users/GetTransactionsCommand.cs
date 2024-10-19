using Application.Economy.Commands;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Users;

public sealed record GetTransactions(DateOnly? date = null) : IRequest<List<TransactionParticipant>>;

public sealed class SendToUserCommandHandler(
    IAppDbContext dbContext,
    IHttpContextAccessor httpContext)
    : IRequestHandler<GetTransactions, List<TransactionParticipant>>
{
    public async Task<List<TransactionParticipant>> Handle(GetTransactions request, CancellationToken ct)
    {
        var claim = httpContext.HttpContext.User.Claims.First(o => o.Type == JwtRegisteredClaimNames.Sid);
        var user = await dbContext.Users.Include(o => o.Accounts)
            .FirstOrDefaultAsync(user => user.Id == Ulid.Parse(claim.Value));

        if (user is null)
        {
            return null;
        }

        var transactions = await dbContext.Set<Account>().Where(o => o.Id == user.Accounts.First().Id).FirstAsync();
        if (request.date is not null)
        {
            return transactions.Transactions.Where(o => o.Transaction.Date.Month == request.date.Value.Month).ToList();
        }

        return transactions.Transactions;
    }
}