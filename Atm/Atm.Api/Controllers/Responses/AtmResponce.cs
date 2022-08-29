using Atm.Api.Controllers.Common;

namespace Atm.Api.Controllers.Responses;

public sealed record AtmResponse(string Message)
{
    public ApiEndpoint[] Links { get; init; } = Array.Empty<ApiEndpoint>();

    public AtmResponse(string message, ApiEndpoint[] links) : this(message) => Links = links;
}