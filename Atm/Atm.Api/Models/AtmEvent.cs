namespace Atm.Api.Models;

public record AtmEvent;
public sealed record InitEvent : AtmEvent;
public sealed record AuthorizeEvent : AtmEvent;
public sealed record WithDrawEvent : AtmEvent;
public sealed record BalanceEvent : AtmEvent;

