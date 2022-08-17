using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;
public class BankService : IBankService
{
    public const int VisaLimit = 200;
    public const int MasterCardLimit = 300;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.Number == cardNumber);

    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number == cardNumber);

    public int GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
            .GetBalance();

    public bool VerifyCardPassword(string cardNumber, string cardPassword)
        => GetCard(cardNumber)
            .IsPasswordEqual(cardPassword);

    public bool VerifyCardLimit(string cardNumber, int amount)
    {
        var card = GetCard(cardNumber);
        return (card.CardBrand, amount) switch
        {
            { CardBrand: CardBrands.Visa, amount: > VisaLimit } => 
                throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {VisaLimit}"),
            { CardBrand: CardBrands.MasterCard, amount: > MasterCardLimit } => 
                throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {MasterCardLimit}"),
            _ => true
        };
    }
}
