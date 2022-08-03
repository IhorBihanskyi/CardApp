using Atm.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atm.Api.Controllers
{
    public sealed record AuthorizeModel(string CardPassword);

    [ApiController]
    [Route("/api/[controller]")]
    public class AtmController : ControllerBase
    {
        private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };

        [HttpGet("cards/{cardNumber}/init")]
        public IActionResult Init(string cardNumber)
        {
            return Cards.Any(x => x.CardNumber == cardNumber)
                ? Ok(new { Message = "Welcome in the system" })
                :NotFound();
        }

        [HttpPost("cards/{cardNumber}/authorize")]
        public IActionResult Authorize([FromRoute] string cardNumber, [FromBody] AuthorizeModel model)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber && x.IsPasswordEqual(model.CardPassword))
                is { }
                ? Ok(new { Message = "Authorization is successfully" })
                : Unauthorized();
        }

        [HttpPut("cards/{cardNumber}/withdraw/{sum}")]
        public IActionResult Withdraw(int sum, string cardNumber)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber) switch
            {
                { } card => Ok(new { Message = $"Balance now is {card.GetBalance() - sum}" }),
                _ => NotFound()
            };
        }

        [HttpGet("cards/{cardNumber}/checkBalance")]
        public IActionResult CheckBalance(string cardNumber)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber) switch
            {
                { } card => Ok(new { Message = $"Balance is {card.GetBalance()}" }),
                _ => NotFound()
            };
        }
    }
}
