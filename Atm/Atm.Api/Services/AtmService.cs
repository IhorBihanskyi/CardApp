using Atm.Api.Models;
using Atm.Api.Interfaces;

namespace Atm.Api.Services;

public sealed class AtmService : IAtmService
{
    private int TotalAmount { get; set; } = 10_000;
    
    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.Number == cardNumber);
    
    private Card GetCard(string cardNumber) => Cards.Single(x => x.Number == cardNumber);

    public decimal GetCardBalance(string cardNumber) 
        => GetCard(cardNumber)
            .GetBalance();
    
    public bool VerifyCardPassword(string cardNumber, string cardPassword) 
        => GetCard(cardNumber)
            .IsPasswordEqual(cardPassword);

    public void Withdraw(string cardNumber, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "");
        }

        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "");
        }

        GetCard(cardNumber).Withdraw(amount);

        TotalAmount -= amount;
    }
}