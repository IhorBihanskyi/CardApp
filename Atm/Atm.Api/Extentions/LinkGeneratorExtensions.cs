using Atm.Api.Controllers.Common;

namespace Atm.Api.Extentions;

public static class LinkGeneratorExtensions
{
    public static ApiEndpoint GetAssociatedEndpoint(
        this LinkGenerator linkGenerator,
        HttpContext httpContext,
        HttpMethod httpMethod,
        string endpointName,
        object? values = null)
    {
        var link = linkGenerator.GetUriByName(httpContext, endpointName, values);

        return new (endpointName, link, httpMethod.Method);
    }
}
