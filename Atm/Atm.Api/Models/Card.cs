namespace Atm.Api.Models
{
    public class Card
    {
        public string CardNumber { get; }
        public string FullName { get; }
        public string CardPassword { get; }
        public CardBrands CardBrand { get; }
        public decimal Balance { get; set; }

        public Card(string cardNumber, string fullName, string cardPassword, CardBrands cardBrand, decimal balance)
        {
            CardNumber = cardNumber;
            FullName = fullName;
            CardPassword = cardPassword;
            CardBrand = cardBrand;
            Balance = balance;
        }

        public bool IsPasswordEqual(string cardPassword) => cardPassword == CardPassword;
        public decimal GetBalance() => Balance;
        public decimal Withdraw(decimal sum) => Balance -= sum;
    }
}