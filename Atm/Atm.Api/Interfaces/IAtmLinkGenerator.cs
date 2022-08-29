using Atm.Api.Controllers.Common;

namespace Atm.Api.Interfaces;

public interface IAtmLinkGenerator
{
    ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null);
}
