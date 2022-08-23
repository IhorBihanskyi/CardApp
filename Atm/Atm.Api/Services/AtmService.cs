using Atm.Api.Interfaces;

namespace Atm.Api.Services;

public sealed class AtmService : IAtmService
{
    private readonly IBankService _bankService;
    private int TotalAmount { get; set; } = 10_000;

    public AtmService(IBankService bankService)
    {
        _bankService = bankService;
    }

    public void Withdraw(string cardNumber, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Invalid amount entered!");
        }

        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
        }

        _bankService.Withdraw(cardNumber, amount);

        TotalAmount -= amount;
    }

    public bool IsCardExist(string cardNumber)
    {
        return _bankService.IsCardExist(cardNumber);
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        return _bankService.VerifyPassword(cardNumber, cardPassword);
    }

    public int GetCardBalance(string cardNumber)
    {
        return _bankService.GetCardBalance(cardNumber);
    }
}