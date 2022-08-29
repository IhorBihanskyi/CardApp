using Atm.Api.Controllers;
using Atm.Api.Controllers.Common;
using Atm.Api.Extentions;

namespace Atm.Api.Services;

public sealed class AtmLinkGenerator
{
    private readonly LinkGenerator _linkGenerator;
    public AtmLinkGenerator(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;

    public ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null)
    {
        return endpointName switch
        {
            nameof(AtmController.Init) => new[]
            {
            _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Authorize))
            },
            nameof(AtmController.Authorize) => new[]
            {
            _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Get, nameof(AtmController.GetBalance), values),
            _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Withdraw))
            },
            nameof(AtmController.GetBalance) => new[]
            {
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Authorize))
            },
            nameof(AtmController.Withdraw) => new[]
            {
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Authorize))
            },
            _ => throw new ArgumentOutOfRangeException("Invalid data!")
        };
    }
}
