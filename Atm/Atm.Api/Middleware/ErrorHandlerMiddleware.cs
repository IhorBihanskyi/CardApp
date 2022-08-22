using System.Net;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
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
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = Application.Json;

            var statusCode = error switch
            {
                InvalidOperationException => Status400BadRequest,
                KeyNotFoundException => Status404NotFound,
                ArgumentOutOfRangeException => Status400BadRequest,
                _ => Status500InternalServerError
            };
            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
