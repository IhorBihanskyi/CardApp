using Atm.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Atm.Api.Controllers.Requests;
using Atm.Api.Controllers.Responses;
using Atm.Api.Services;

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

    [HttpGet("{cardNumber}/init", Name = nameof(Init))]
    public IActionResult Init([FromServices] AtmLinkGenerator linkGenerator, [FromRoute] string cardNumber)
    {

        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Init));

        return _atmService.IsCardExist(cardNumber)
            ? Ok(new AtmResponse($"Your card in the system!", links))
            : NotFound(new AtmResponse("Your card isn't in the system!"));
    }

    [HttpPost("authorize", Name = nameof(Authorize))]
    public IActionResult Authorize([FromServices] AtmLinkGenerator linkGenerator, [FromBody] CardAuthorizeRequest request)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Authorize), new { request.CardNumber });

        return _atmService.VerifyPassword(request.CardNumber, request.CardPassword)
        ? Ok(new AtmResponse($" Authorization was successfully!", links))
        : Unauthorized(new AtmResponse("Invalid password"!));
    }

    [HttpPost("withdraw", Name = nameof(Withdraw))]
    public IActionResult Withdraw([FromServices] AtmLinkGenerator linkGenerator, [FromBody] CardWithdrawRequest request)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Init));

        _atmService.Withdraw(request.CardNumber, request.Amount);
        return Ok(new AtmResponse("The operation was successfully!", links));
    }

    [HttpGet("{cardNumber}/balance", Name = nameof(GetBalance))]
    public IActionResult GetBalance([FromServices] AtmLinkGenerator linkGenerator, [FromRoute] string cardNumber)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Init));

        var balance = _atmService.GetCardBalance(cardNumber);
        return Ok(new AtmResponse($"Balance is {balance}", links));
    }
}
