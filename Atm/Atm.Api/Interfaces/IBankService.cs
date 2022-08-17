using Atm.Api.Models;

namespace Atm.Api.Interfaces
{
    public interface IBankService
    {
        bool IsCardExist(string cardNumber);
        int GetCardBalance(string cardNumber);
        bool VerifyCardPassword(string cardNumber, string cardPassword);
        Card GetCard(string cardNumber);
        bool VerifyCardLimit(string cardNumber, int amount);
    }
}
