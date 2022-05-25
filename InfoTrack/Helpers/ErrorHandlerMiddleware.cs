namespace InfoTrack.Helpers;

using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
    {
        _next = next;
        _logger = logger;
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
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                AppException => (int)HttpStatusCode.BadRequest, // custom application error
                KeyNotFoundException => (int)HttpStatusCode.NotFound, // not found error
                BadHttpRequestException => ((BadHttpRequestException)error).StatusCode, // retain status code
                _ => (int)HttpStatusCode.InternalServerError, // unhandled error
            };
            var result = JsonSerializer.Serialize(new { message = error?.Message });
            _logger.LogError(error, result, new string[] { "ErrorHandlerMiddleware", $"{context.Response}" });
            await response.WriteAsync(result);
        }
    }
}
