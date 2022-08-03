using Atm.Api.Models;
using Atm.Api.Services;
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
        
        // /api/atm/cards/4444333322221111/init
        
        // "some message"
        // { "message": "some message", "test": 12345 }
        
        // { "message": "some message", "test": 12345 }
        
        [HttpGet("cards/{cardNumber}/init")]
        public IActionResult Init(string cardNumber)
        {
            return Cards.Any(x => x.CardNumber == cardNumber)
                ? Ok(new { Message = "Card was initialized successfully" })
                : NotFound();
            
            return Cards.Any(x => x.CardNumber == cardNumber) switch
            {
                true => Ok(),
                _ => BadRequest()
            };
            
            if (Cards.Any(x => x.CardNumber == cardNumber))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("cards/{cardNumber}/authorize")]
        public IActionResult Authorize([FromRoute] string cardNumber, [FromBody] AuthorizeModel model)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber && x.IsPasswordEqual(model.CardPassword))
                is { }
                ? Ok()
                : Unauthorized();
        }

        [HttpPut("Init/Authorize/Withdraw")]
        public IActionResult Withdraw(int sum, string cardNumber)
        {
            
        }

        [HttpGet("Init/Authorize/CheckBalance")]
        public IActionResult CheckBalance(string cardNumber)
        {
            var balance = Cards
                .Single(x => x.CardNumber == cardNumber)
                .GetBalance();
            
            return Ok(balance);
        }
    }
}
