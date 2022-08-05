namespace Atm.Api.Models
{
    public class Card
    {
        public const int TotalAmount = 10000;

        public string Number { get; }
        public string FullName { get; }
        public string CardPassword { get; }
        public CardBrands CardBrand { get; }
        public decimal Balance { get; set; }

        public Card(string cardNumber, string fullName, string cardPassword, CardBrands cardBrand, decimal balance)
        {
            Number = cardNumber;
            FullName = fullName;
            CardPassword = cardPassword;
            CardBrand = cardBrand;
            Balance = balance;
        }

        public bool IsPasswordEqual(string cardPassword) => cardPassword == CardPassword;
        public decimal GetBalance() => Balance;
        public decimal Withdraw(decimal amount)
        {
            return amount switch
            {
                <= 0 => throw new ArgumentOutOfRangeException("You could not withdraw less or equal to zero"),
                 > TotalAmount => throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!"),
                _ => Balance -= amount
            };
        }
    };
}