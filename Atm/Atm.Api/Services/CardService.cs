using Atm.Api.Models;
using Newtonsoft.Json;

namespace Atm.Api.Services
{
    public class CardService : ICardService
    {
        public List<Card> GetCards()
        {
            using var stream = File.OpenText("cards.json");
            using var reader = new JsonTextReader(stream);
            return new JsonSerializer().Deserialize<List<Card>>(reader);
        }
    }
}
