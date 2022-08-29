using Atm.Api.Controllers;
using Atm.Api.Controllers.Common;
using Atm.Api.Extentions;
using Atm.Api.Interfaces;

namespace Atm.Api.Services;

public sealed class AtmLinkGenerator : IAtmLinkGenerator
{
    private readonly LinkGenerator _linkGenerator;
    public AtmLinkGenerator(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;
    
    public ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null)
    {
        return endpointName switch
        {
            nameof(AtmController.Init) => GetAuthorizeLink(httpContext),
            nameof(AtmController.Authorize) => GetBalanceWithdrawLink(httpContext, values),
            nameof(AtmController.GetBalance) => GetAuthorizeLink(httpContext),
            nameof(AtmController.Withdraw) => GetAuthorizeLink(httpContext),
            _ => throw new ArgumentOutOfRangeException("Invalid data!")
        };
    }

    private ApiEndpoint[] GetAuthorizeLink(HttpContext httpContext)
    {
        return new[]
            {
            _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Authorize))
            };
    }

    private ApiEndpoint[] GetBalanceWithdrawLink(HttpContext httpContext, object? values = null)
    {
        return new[]
            {
            _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Get, nameof(AtmController.GetBalance), values),
            _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Withdraw))
            };
    }
}
