using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;

public class AtmEventService : IAtmService
{
    private readonly IAtmService _atm;
    private readonly IAtmEventBroker _broker;

    public AtmEventService(IAtmService atm, IAtmEventBroker broker) => (_atm, _broker) = (atm, broker);

    public bool IsCardExist(string cardNumber)
    {
        if (!_atm.IsCardExist(cardNumber))
        {
            return false;
        }
        _broker.StartStream(cardNumber, new AtmEvent());
        _broker.AppendEvent(cardNumber, new InitEvent());
        return true;
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        _broker.FindEvent<InitEvent>(cardNumber);

        if (_atm.VerifyPassword(cardNumber, cardPassword))
        {
            _broker.AppendEvent(cardNumber, new AuthorizeEvent());
            return true;
        }
        return false;
    }

    public int GetCardBalance(string cardNumber)
    {
        var @event = _broker.GetLastEvent(cardNumber);
        if (@event is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation!");
        }
        _broker.AppendEvent(cardNumber, new BalanceEvent());

        return _atm.GetCardBalance(cardNumber);
    }

    public void Withdraw(string cardNumber, int amount)
    {
        var @event = _broker.GetLastEvent(cardNumber);
        if (@event is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation!");
        }
        _atm.Withdraw(cardNumber, amount);
        _broker.AppendEvent(cardNumber, new WithDrawEvent());
    }
}
