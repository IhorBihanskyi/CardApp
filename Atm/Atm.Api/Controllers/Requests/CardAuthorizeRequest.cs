namespace Atm.Api.Controllers.Requests
{
    public sealed record CardAuthorizeRequest(string CardPassword, string cardNumber);
}
