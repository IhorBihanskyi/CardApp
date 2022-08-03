namespace Atm.Api.Controllers.Responses
{
    public class AtmResponce
    {
        public string Message { get; }

        public AtmResponce(string message)
        {
            Message = message;
        }
    }
}
