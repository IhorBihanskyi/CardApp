namespace Atm.Api.Controllers.Requests
{
    public sealed record CardWithdrawRequest(decimal sum, string cardNumber);
}
