using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;
public class BankService : IBankService
{
    private readonly IAtmService _atmService;
    private int VisaLimit { get; set; } = 200;
    private int MasterCardLimit { get; set; } = 300;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public BankService(IAtmService atmService)
    {
        _atmService = atmService;
    }

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.Number == cardNumber);

    private Card GetCard(string cardNumber) => Cards.Single(x => x.Number == cardNumber);

    public int GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
            .GetBalance();

    public bool VerifyCardPassword(string cardNumber, string cardPassword)
        => GetCard(cardNumber)
            .IsPasswordEqual(cardPassword);

    public void Withdraw(string cardNumber, int amount)
    {
        var card = GetCard(cardNumber);

        if (card.CardBrand == CardBrands.Visa && amount > VisaLimit)
        {
            throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {VisaLimit}");
        }
        if (card.CardBrand == CardBrands.MasterCard && amount > MasterCardLimit)
        {
            throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {MasterCardLimit}");
        }

        _atmService.AtmWithdraw(amount);
        card.Withdraw(amount);
    }
}
