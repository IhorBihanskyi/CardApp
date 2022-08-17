using Atm.Api.Helpers;
using System.Net;
using System.Text.Json;

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
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = Constants.contentType;

            var statusCode = error switch
            {
                InvalidOperationException => HttpStatusCode.NotFound,
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentOutOfRangeException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
