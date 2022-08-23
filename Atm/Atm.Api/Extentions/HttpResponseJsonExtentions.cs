namespace Atm.Api.Extentions;

public static class HttpResponseJsonExtentions
{
    public static HttpResponse WithStatusCode(this HttpResponse response, int statusCode)
    {
        response.StatusCode = statusCode;
        return response;
    }

    public static Task WithJsonContent(this HttpResponse response, object content)
    {
        return response.WriteAsJsonAsync(new { content });
    }
}
