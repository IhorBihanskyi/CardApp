namespace Atm.Api.Controllers.Requests;

public sealed record CardWithdrawRequest(string CardNumber, int Amount);