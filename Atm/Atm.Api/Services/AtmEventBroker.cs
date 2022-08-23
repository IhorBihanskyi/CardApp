using Atm.Api.Interfaces;
using Atm.Api.Models;

namespace Atm.Api.Services;

public class AtmEventBroker : IAtmEventBroker
{
    private readonly IDictionary<string, ICollection<AtmEvent>> _events
        = new Dictionary<string, ICollection<AtmEvent>>();

    public void StartStream(string key, AtmEvent @event)
    {
        _events.Remove(key);
        _events.Add(key, new List<AtmEvent> { @event });
    }

    public void AppendEvent(string key, AtmEvent @event)
    {
        _events[key].Add(@event);
    }

    public AtmEvent? FindEvent<T>(string key) where T : AtmEvent
    {
        return _events[key].FirstOrDefault();
    }

    public AtmEvent GetLastEvent(string key)
    {
        return _events[key].Last();
    }
}
