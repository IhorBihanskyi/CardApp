namespace Atm.Api.Interfaces;

public interface IAtmService
{
    void Withdraw(string cardNumber, int amount);
}