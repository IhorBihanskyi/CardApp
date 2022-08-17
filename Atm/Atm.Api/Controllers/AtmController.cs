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
    private readonly IBankService _bankService;
    public AtmController(IAtmService atmService, IBankService bankService)
    {
        _atmService = atmService;
        _bankService = bankService;
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init(string cardNumber)
    {
        return _bankService.IsCardExist(cardNumber)
            ? Ok(new AtmResponse("Welcome in the system!"))
            : NotFound(new AtmResponse("Your card isn't in the system!"));
    }

    [HttpPost("authorize")]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        return _bankService.VerifyCardPassword(request.CardNumber, request.CardPassword)
            ? Ok(new AtmResponse("Authorization was successfully!"))
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
        var balance = _bankService.GetCardBalance(cardNumber);

        return Ok(new AtmResponse($"Balance is {balance}"));
    }
}
