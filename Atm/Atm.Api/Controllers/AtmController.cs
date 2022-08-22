using Atm.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Atm.Api.Controllers.Requests;
using Atm.Api.Controllers.Responses;
using Microsoft.Extensions.Caching.Memory;
using Atm.Api.Models;

namespace Atm.Api.Controllers;

[ApiController]
[Route("/api/[controller]/cards/")]
public class AtmController : ControllerBase
{
    private const string initKey = "init";
    private const string authorizeKey = "author";
    private readonly IAtmService _atmService;
    private readonly IBankService _bankService;
    private IMemoryCache _cache;

    public AtmController(IAtmService atmService, IBankService bankService, IMemoryCache cache)
    {
        _atmService = atmService;
        _bankService = bankService;
        _cache = cache;
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init(string cardNumber)
    {
        return _bankService.IsCardExist(cardNumber)
            ? Ok(new AtmResponse($"Your card {_cache.Set(initKey, cardNumber)} in the system!"))
            : NotFound(new AtmResponse("Your card isn't in the system!"));
    }

    [HttpPost("authorize")]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        string token = string.Empty;
        if (_cache.TryGetValue(initKey, out token))
        {
            _cache.Remove(initKey);

            return _bankService.VerifyCardPassword(request.CardNumber, request.CardPassword)
            ? Ok(new AtmResponse($"{_cache.Set(authorizeKey, request.CardPassword)} Authorization was successfully!"))
            : Unauthorized(new AtmResponse("Invalid password"!));
        }
        return BadRequest();
            
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        string token = string.Empty;
        if (_cache.TryGetValue(authorizeKey, out token))
        {
            _cache.Remove(authorizeKey);

            _atmService.Withdraw(request.CardNumber, request.Amount);
            return Ok(new AtmResponse("The operation was successfully!"));
        }
        return BadRequest();
    }

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance(string cardNumber)
    {
        string token = string.Empty;
        if (_cache.TryGetValue(authorizeKey, out token))
        {
            _cache.Remove(authorizeKey);

            var balance = _bankService.GetCardBalance(cardNumber);
            return Ok(new AtmResponse($"Balance is {balance}"));
        }
        return BadRequest();
        
    }
}
