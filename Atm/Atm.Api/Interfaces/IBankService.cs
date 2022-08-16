namespace Atm.Api.Interfaces
{
    public interface IBankService
    {
        bool IsCardExist(string cardNumber);
        int GetCardBalance(string cardNumber);
        bool VerifyCardPassword(string cardNumber, string cardPassword);
        void Withdraw(string cardNumber, int amount);
    }
}
