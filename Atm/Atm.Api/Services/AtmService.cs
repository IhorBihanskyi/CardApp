using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services
{
    public class AtmService : IAtmService
    {
        private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };

        public bool IsCardNumberExist(string cardNumber) => Cards.Any(x => x.Number == cardNumber);
        public Card FindCard(string cardNumber) => Cards.SingleOrDefault(x => x.Number == cardNumber);

        public bool AuthorizeCard(string cardNumber, string cardPassword)
        {
            return FindCard(cardNumber) switch
            {
                { } card => card.IsPasswordEqual(cardPassword)
                ? true
                : false
            };
        }
    }
}
