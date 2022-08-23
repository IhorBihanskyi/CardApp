using Atm.Api.Interfaces;
using Atm.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Atm.Api.Services;
public class BankService : IBankService
{
    public const int VisaLimit = 200;
    public const int MasterCardLimit = 300;

    private IMemoryCache _cache;
    private const string initKey = "init";
    private const string authorizeKey = "author";

    public BankService( IMemoryCache cache)
    {
        _cache = cache;
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
            _cache.Set(initKey, cardNumber);
            return true;
        }

        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public decimal GetCardBalance(string cardNumber)
    {
        if (_cache.TryGetValue(authorizeKey, out string token))
        {
            _cache.Remove(authorizeKey);
            return GetCard(cardNumber).GetBalance();
        }

        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        if (_cache.TryGetValue(initKey, out string token) && GetCard(cardNumber).IsPasswordEqual(cardPassword))
        {
            _cache.Remove(initKey);
            _cache.Set(authorizeKey, cardPassword);
            return true;
        }

        throw new InvalidOperationException("Pass identification and authorization!");
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

        card.Withdraw(amount);
    }
}
