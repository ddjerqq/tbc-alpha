using Application.Auth;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

/// <summary>
/// Controller for authentication actions
/// </summary>
public sealed class AuthController(ILogger<ApiController> logger, IMediator mediator) : ApiController(logger)
{
    /// <summary>
    /// Gets the user's claims
    /// </summary>
    [HttpGet("claims")]
    public ActionResult<Dictionary<string, string>> GetUserClaims() => Ok(User.Claims.ToDictionary(c => c.Type, c => c.Value));

    /// <summary>
    /// Logs the user in
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginCommand command, CancellationToken ct)
    {
        var response = await mediator.Send(command, ct);

        switch (response)
        {
            case LoginResponse.Success { Token: var token, User: var user }:
                Response.Cookies.Append("authorization", token);
                return Ok(user);
            // case LoginResponse.TwoFactorRequired:
            //     return Redirect()
            case LoginResponse.Failure:
                return BadRequest("bad credentials");
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}