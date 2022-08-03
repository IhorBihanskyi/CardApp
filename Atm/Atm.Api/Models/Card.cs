namespace Atm.Api.Models
{
    public class Card
    {
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string CardPassword { get; set; }
        public CardBrands CardBrand { get; set; }
        public int Balance { get; set; }

        public Card(string cardNumber, string fullName, string cardPassword, CardBrands cardBrand, int balance)
        {
            CardNumber = cardNumber;
            FullName = fullName;
            CardPassword = cardPassword;
            CardBrand = cardBrand;
            Balance = balance;
        }

        public bool IsPasswordEqual(string cardPassword) => cardPassword == CardPassword;
        public int GetBalance() => Balance;
    }
}