namespace Atm.Api.Controllers.Requests
{
    public sealed record CardWithdrawRequest(decimal Amount, string CardNumber);
}
