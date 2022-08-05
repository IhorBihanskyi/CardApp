using Atm.Api.Models;

namespace Atm.Api.Interfaces
{
    public interface IAtmService
    {
        bool IsCardNumberExist(string cardNumber);
        Card FindCard(string cardNumber);
        bool AuthorizeCard(string cardNumber, string cardPassword);
    }
}
