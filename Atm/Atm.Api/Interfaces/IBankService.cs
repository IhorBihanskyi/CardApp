using Atm.Api.Models;

namespace Atm.Api.Interfaces;

public interface IBankService
{
    bool IsCardExist(string cardNumber);
    bool VerifyPassword(string cardNumber, string cardPassword);
    void Withdraw(string cardNumber, int amount);
    int GetCardBalance(string cardNumber);
    Card GetCard(string cardNumber);
}
