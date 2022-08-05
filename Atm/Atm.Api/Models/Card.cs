namespace Atm.Api.Models
{
    public class Card
    {
        public string Number { get; }
        public string FullName { get; }
        private string CardPassword { get; }
        public CardBrands CardBrand { get; }
        private decimal Balance { get; set; }

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
        
        public void Withdraw(decimal amount)
        {
            if (amount > GetBalance())
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "");
            }

            Balance -= amount;
        }
    };
}