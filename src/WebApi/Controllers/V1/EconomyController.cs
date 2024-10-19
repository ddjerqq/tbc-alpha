using Application.Economy.Commands;
using Application.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers.v1;

/// <summary>
/// 
/// </summary>
/// <param name="mediator"></param>
public class EconomyController(ILogger<ApiController> logger, IMediator mediator) : ApiController(logger)
{
    [HttpPost("send")]
    public async Task<ActionResult> Send([FromBody] SendToUserCommand command, CancellationToken ct)

    {
        var resp = await mediator.Send(command, ct);
        if (resp)
        {
            return Ok("Success");
        }

        return BadRequest("Failed");
    }

    [HttpPost("send")]
    public async Task<ActionResult> GetTransactions(GetTransactions transactions, CancellationToken ct)

    {
        var resp = await mediator.Send(transactions, ct);
        if (resp.Count == 0)
        {
            return BadRequest("Failed");
        }

        return Ok(resp);
    }
}