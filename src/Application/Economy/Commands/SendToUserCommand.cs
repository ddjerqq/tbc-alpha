using Application.Services;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Economy.Commands;

public sealed record SendToUserCommand(Iban Sender, Iban Receiver, Money Amount) : IRequest<bool>;

public sealed class SendToUserCommandHandler(
    IAppDbContext dbContext,
    IHttpContextAccessor httpContext,
    IMediator mediator)
    : IRequestHandler<SendToUserCommand, bool>
{
    public async Task<bool> Handle(SendToUserCommand request, CancellationToken ct)
    {
        var claim = httpContext.HttpContext.User.Claims.First(o => o.Type == JwtRegisteredClaimNames.Sid);
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == Ulid.Parse(claim.Value));


        var receiver = await dbContext.Set<Account>()
            .FindAsync(request.Receiver);

        var sender = await dbContext.Set<Account>().FindAsync(request.Sender);
        if (user is null || receiver is null)
        {
            return false;
        }

        var response = await mediator.Send(new BalanceTransactionCommand(sender, receiver, request.Amount), ct);
        return response;
    }
}