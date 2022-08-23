using System.Net;
using System.Text.Json;
using Atm.Api.Extentions;
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
        catch (InvalidOperationException ex)
        {
            await context.Response
                .WithStatusCode(Status422UnprocessableEntity)
                .WithJsonContent(ex.Message);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = Application.Json;

            var statusCode = error switch
            {
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
