namespace Atm.Api.Interfaces;

public interface IAtmService
{
    // bool IsCardExist(string cardNumber);

    // bool VerifyPassword(string cardNumber, string cardPassword);

    // void Withdraw(string cardNumber, decimal amount);

    // decimal GetCardBalance(string cardNumber);

    void Withdraw(string cardNumber, int amount);
}