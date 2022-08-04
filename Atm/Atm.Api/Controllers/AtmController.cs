using Atm.Api.Controllers.Requests;
using Atm.Api.Controllers.Responses;
using Atm.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atm.Api.Controllers
{

    [ApiController]
    [Route("/api/[controller]/cards/")]
    public class AtmController : ControllerBase
    {
        private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };

        [HttpGet("{cardNumber}/init")]
        public IActionResult Init(string cardNumber)
        {
            return Cards.Any(x => x.CardNumber == cardNumber)
                ? Ok(new { Message = "Welcome in the system!" })
                : NotFound();
        }

        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == request.CardNumber) switch
            {
                { } card => card.IsPasswordEqual(request.CardPassword)
                ? Ok(new AtmResponce("Authorization was successfully!"))
                : Unauthorized(new AtmResponce("Invalid password!")),
                _ => NotFound()
            };
        }

        [HttpPost("{cardNumber}/withdraw/{sum}")]
        public IActionResult Withdraw([FromRoute] CardWithdrawRequest request)
        {
            var card = Cards.SingleOrDefault(x => x.CardNumber == request.CardNumber);

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
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber) switch
            {
                { } card => Ok(new AtmResponce($"Balance is {card.GetBalance()}")),
                _ => NotFound()
            };
        }
    }
}
