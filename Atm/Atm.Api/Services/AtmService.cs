using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;

public sealed class AtmService : IAtmService
{
    private readonly IBankService _bankService;

    private int TotalAmount { get; set; } = 10_000;

    public AtmService(IBankService bankService) =>
        _bankService = bankService;


    public bool IsCardExist(string cardNumber) =>
         _bankService.IsCardExist(cardNumber);


    public bool VerifyPassword(string cardNumber, string cardPassword) =>
        _bankService.VerifyPassword(cardNumber, cardPassword);


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

    public int GetCardBalance(string cardNumber) =>
        _bankService.GetCardBalance(cardNumber);

}