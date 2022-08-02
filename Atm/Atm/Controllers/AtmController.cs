using Atm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Atm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtmController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IUserAccessorService _userAccessorService;
        public AtmController(ICardService cardService, IUserAccessorService userAccessorService)
        {
            _cardService = cardService;
            _userAccessorService = userAccessorService;
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

        [HttpPut("Init/Authorize/Withdraw")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Withdraw(int sum)
        {
            var accessor = _userAccessorService.GetCardAccess();
            var card = _cardService.GetCards().FirstOrDefault(s => s.FullName == accessor);
            int money = card.Money - sum;
            return Ok(money);
        }

        [HttpGet("Init/Authorize/CheckBalance")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult CheckBalance()
        {
            var accessor = _userAccessorService.GetCardAccess();
            var card = _cardService.GetCards().FirstOrDefault(x => x.FullName == accessor);
            var balance = card.Money;
            return Ok(balance);
        }
    }
}
