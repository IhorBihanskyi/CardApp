using System.Security.Claims;

namespace Atm.Api.Services
{
    public class AccessorService: IUserAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCardAccess()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
