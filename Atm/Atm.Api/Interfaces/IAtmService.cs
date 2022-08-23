namespace Atm.Api.Interfaces;

public interface IAtmService
{
    bool IsCardExist(string cardNumber);
    bool VerifyPassword(string cardNumber, string cardPassword);
    int GetCardBalance(string cardNumber);
    void Withdraw(string cardNumber, int amount);
}