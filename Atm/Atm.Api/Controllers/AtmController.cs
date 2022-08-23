using Atm.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Atm.Api.Controllers.Requests;
using Atm.Api.Controllers.Responses;

namespace Atm.Api.Controllers;

[ApiController]
[Route("/api/[controller]/cards/")]
public class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService)
    {
        _atmService = atmService;
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init(string cardNumber)
    {
        return _atmService.IsCardExist(cardNumber)
            ? Ok(new AtmResponse($"Your card in the system!"))
            : NotFound(new AtmResponse("Your card isn't in the system!"));
    }

    [HttpPost("authorize")]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        return _atmService.VerifyPassword(request.CardNumber, request.CardPassword)
        ? Ok(new AtmResponse($" Authorization was successfully!"))
        : Unauthorized(new AtmResponse("Invalid password"!));
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        _atmService.Withdraw(request.CardNumber, request.Amount);
        return Ok(new AtmResponse("The operation was successfully!"));
    }

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance(string cardNumber)
    {
        var balance = _atmService.GetCardBalance(cardNumber);
        return Ok(new AtmResponse($"Balance is {balance}"));
    }
}
