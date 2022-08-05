namespace Atm.Api.Interfaces;

public interface IAtmService
{
    bool IsCardExist(string cardNumber);
    decimal GetCardBalance(string cardNumber);
    bool VerifyCardPassword(string cardNumber, string cardPassword);
    void Withdraw(string cardNumber, int amount);
}