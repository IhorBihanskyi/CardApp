namespace Atm.Api.Controllers.Common;

public sealed record ApiEndpoint(
    string Rel,
    string? Href,
    string Method);

