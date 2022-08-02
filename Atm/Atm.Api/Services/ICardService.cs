using Atm.Api.Models;

namespace Atm.Api.Services
{
    public interface ICardService
    {
        public List<Card> GetCards();
    }
}
