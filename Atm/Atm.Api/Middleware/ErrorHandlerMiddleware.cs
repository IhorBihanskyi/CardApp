using Atm.Api.Extentions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Atm.Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            await context.Response
                .WithStatusCode(Status422UnprocessableEntity)
                .WithJsonContent(ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await context.Response
                .WithStatusCode(Status413PayloadTooLarge)
                .WithJsonContent(ex.Message);
        }
    }
}
