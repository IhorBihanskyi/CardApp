using Atm.Api.Controllers.Requests;
using Atm.Api.Controllers.Responses;
using Atm.Api.Interfaces;
using Atm.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atm.Api.Controllers
{

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
            return _atmService.IsCardNumberExist(cardNumber)
                ? Ok(new { Message = "Welcome in the system!" })
                : NotFound();
        }

        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
        {
            return _atmService.FindCard(request.CardNumber) switch
            {
                { } card => card.IsPasswordEqual(request.CardPassword)
                ? Ok(new AtmResponce("Authorization was successfully!"))
                : Unauthorized(new AtmResponce("Invalid password!")),
                _ => NotFound()
            };
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
        {
            var card = _atmService.FindCard(request.CardNumber);
            if (card is null)
            {
                return NotFound();
            }

                card.Withdraw(request.Amount);

            return Ok(new AtmResponce("The operation was successfully!"));
        }

        [HttpGet("{cardNumber}/balance")]
        public IActionResult CheckBalance(string cardNumber)
        {
            return _atmService.FindCard(cardNumber) switch
            {
                { } card => Ok(new AtmResponce($"Balance is {card.GetBalance()}")),
                _ => NotFound()
            };
        }
    }
}
