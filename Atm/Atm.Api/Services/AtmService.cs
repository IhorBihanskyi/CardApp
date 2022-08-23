using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;

public sealed class AtmService : IAtmService
{
    private readonly IBankService _bankService;
    private IAtmEventBroker _broker;

    private int TotalAmount { get; set; } = 10_000;

    public AtmService(IBankService bankService, IAtmEventBroker broker)
    {
        _bankService = bankService;
        _broker = broker;
    }

    public void Withdraw(string cardNumber, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Invalid amount entered!");
        }

        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
        }

        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Pass identification and authorization!");
        }
        _broker.AppendEvent(cardNumber, new WithDrawEvent());

        _bankService.Withdraw(cardNumber, amount);

        TotalAmount -= amount;
    }

    public bool IsCardExist(string cardNumber)
    {
        if (_bankService.IsCardExist(cardNumber))
        {
            _broker.StartStream(cardNumber, new InitEvent());
            return true;
        }
        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        if (_broker.FindEvent<InitEvent>(cardNumber) is not null && _bankService.VerifyPassword(cardNumber, cardPassword))
        {
            _broker.AppendEvent(cardNumber, new AuthorizeEvent());
            return true;
        }
        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public int GetCardBalance(string cardNumber)
    {
        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Pass identification and authorization!");
        }
        _broker.AppendEvent(cardNumber, new BalanceEvent());
        return _bankService.GetCardBalance(cardNumber);
    }
}