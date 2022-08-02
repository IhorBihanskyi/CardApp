using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Atm.Api.Models
{
    public class Card
    {
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CardBrands CardBrand { get; set; }
        public int Money { get; set; }
    }
}
