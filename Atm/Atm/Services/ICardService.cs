using Atm.Models;

namespace Atm.Services
{
    public interface ICardService
    {
        public List<Card> GetCards();
        string Authenticate(string password);
    }
}
