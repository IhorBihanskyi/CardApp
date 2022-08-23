using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;
public class BankService : IBankService
{
    public const int VisaLimit = 200;
    public const int MasterCardLimit = 300;

    private IAtmEventBroker _broker;

    public BankService(IAtmEventBroker broker)
    {
        _broker = broker;
    }

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    private static readonly IReadOnlyCollection<CardBrandLimit> WithdrawLimits = new List<CardBrandLimit>
    {
        new (CardBrands.Visa, 200),
        new (CardBrands.MasterCard, 300)
    };

    private static decimal GetWithdrawLimit(CardBrands cardBrand)
    {
        return WithdrawLimits.First(x => x.CardBrand == cardBrand).Amount;
    }

    public bool IsCardExist(string cardNumber)
    {
        if (Cards.Any(x => x.Number == cardNumber))
        {
            _broker.StartStream(cardNumber, new InitEvent());
            return true;
        }
        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        if (_broker.FindEvent<InitEvent>(cardNumber) is not null && GetCard(cardNumber).IsPasswordEqual(cardPassword))
        {
            _broker.AppendEvent(cardNumber, new AuthorizeEvent());
            return true;
        }
        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public int GetCardBalance(string cardNumber)
    {
        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Pass identification and authorization!");
        }
        _broker.AppendEvent(cardNumber, new BalanceEvent());
        return GetCard(cardNumber).GetBalance();
    }

    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number == cardNumber);

    public void Withdraw(string cardNumber, int amount)
    {
        var card = GetCard(cardNumber);
        var limit = GetWithdrawLimit(card.CardBrand);

        if (amount > limit)
        {
            throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {limit}");
        }
        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Pass identification and authorization!");
        }
        _broker.AppendEvent(cardNumber, new WithDrawEvent());
        card.Withdraw(amount);
    }
}
