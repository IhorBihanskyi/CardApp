using Atm.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atm.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtmController : ControllerBase
    {
        private readonly ICardService _cardService;
        public AtmController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("Init")]
        public ActionResult Init(string cardNumber)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.CardNumber == cardNumber);
            _ = card ?? throw new ArgumentException("Can`t find this card");

            return Ok();
        }

        [HttpPost("Init/Authorize")]
        public IActionResult Authorize([FromForm] string password)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.Password == password);
            _ = card ?? throw new ArgumentException("Password incorrect");
            return Ok();
        }

        [HttpPut("Init/Authorize/Withdraw")]
        public ActionResult Withdraw(int sum, string cardNumber)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.CardNumber == cardNumber);
            int money = card.Money - sum;
            return Ok(money);
        }

        [HttpGet("Init/Authorize/CheckBalance")]
        public ActionResult CheckBalance(string cardNumber)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.CardNumber == cardNumber);
            var balance = card.Money;
            return Ok(balance);
        }
    }
}
