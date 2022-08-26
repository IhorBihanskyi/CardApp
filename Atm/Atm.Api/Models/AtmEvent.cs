namespace Atm.Api.Models;

public abstract record AtmEvent;
public sealed record InitEvent : AtmEvent;
public sealed record AuthorizeEvent : AtmEvent;
public sealed record WithDrawEvent : AtmEvent;
public sealed record BalanceEvent : AtmEvent;
