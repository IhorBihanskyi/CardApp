namespace Atm.Api.Models
{
    public class Card
    {
        public string CardNumber { get; }
        public string FullName { get; }
        public string Password { get; }
        public CardBrands CardBrand { get; }
        public int Balance { get; }

        public Card(string cardNumber, string fullName, string password, CardBrands cardBrand, int balance)
        {
            CardNumber = cardNumber;
            FullName = fullName;
            Password = password;
            CardBrand = cardBrand;
            Balance = balance;
        }

        public bool IsPasswordEqual(string cardPassword) => cardPassword == Password;

        public int GetBalance() => Balance;
    }
}
