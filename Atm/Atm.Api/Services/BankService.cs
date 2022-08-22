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

    public bool IsCardExist(string cardNumber) 
    {
        if(Cards.Any(x => x.Number == cardNumber))
        {
            _cache.Set(initKey, cardNumber);
            return true;
        }
        throw new ArgumentOutOfRangeException("Not allowed operation!");
    }

    public Card GetCard(string cardNumber) => Cards.Single(x => x.Number == cardNumber);

    public bool VerifyCardPassword(string cardNumber, string cardPassword)
    {
        string token = string.Empty;
        if (_cache.TryGetValue(initKey, out token) && GetCard(cardNumber).IsPasswordEqual(cardPassword))
        {
            _cache.Remove(initKey);
            _cache.Set(authorizeKey, cardPassword);
            return true;
        }
        throw new ArgumentOutOfRangeException("Not allowed operation!");
    }

    public int GetCardBalance(string cardNumber)
    {
        string token = string.Empty;
        if (_cache.TryGetValue(authorizeKey, out token))
        {
            _cache.Remove(authorizeKey);
            return GetCard(cardNumber).GetBalance();
        }
        throw new ArgumentOutOfRangeException("Not allowed operation!");
    }

    public bool VerifyCardLimit(string cardNumber, int amount)
    {
        var card = GetCard(cardNumber);

        string token = string.Empty;
        if (_cache.TryGetValue(authorizeKey, out token))
        {
            _cache.Remove(authorizeKey);
            return (card.CardBrand, amount) switch
            {
                { CardBrand: CardBrands.Visa, amount: > VisaLimit } =>
                    throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {VisaLimit}"),
                { CardBrand: CardBrands.MasterCard, amount: > MasterCardLimit } =>
                    throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {MasterCardLimit}"),
                _ => true
            };
        }
        throw new ArgumentOutOfRangeException("Not allowed operation!");
    }
}
