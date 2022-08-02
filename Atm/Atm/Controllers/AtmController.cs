using Atm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtmController : Controller
    {
        private readonly ICardService _cardService;
        public AtmController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("Init")]
        [AllowAnonymous]
        public ActionResult Init(string cardNumber)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.CardNumber == cardNumber);
            _ = card ?? throw new ArgumentException("Can`t find this card");

            return Ok();
        }

        [HttpPost]
        [Route("Init/Authorize")]
        [AllowAnonymous]
        public IActionResult Authorize([FromForm] string password)
        {
            var authResult = _cardService.Authenticate(password);
            return Ok(authResult);
        }

        [HttpPost("Init/Authorize/Withdraw")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Withdraw(int sum, int id)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.Id == id);
            int money = card.Money - sum;
            return Ok(money);
        }

        [HttpGet("InitAuthorize/CheckBalance")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult CheckBalance(int id)
        {
            var card = _cardService.GetCards().FirstOrDefault(s => s.Id == id);
            return Ok(card.Money);
        }
    }
}
