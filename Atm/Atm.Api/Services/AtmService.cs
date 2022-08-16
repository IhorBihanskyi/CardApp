using Atm.Api.Models;
using Atm.Api.Interfaces;

namespace Atm.Api.Services;

public sealed class AtmService : IAtmService
{
    private int TotalAmount { get; set; } = 10_000;
    
    public void AtmWithdraw(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Invalid amount entered!");
        }

        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
        }
        TotalAmount -= amount;
    }
}