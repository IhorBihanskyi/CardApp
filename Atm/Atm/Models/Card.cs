namespace Atm.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public CardBrands CardBrand { get; set; }
        public int Money { get; set; }
    }
}
