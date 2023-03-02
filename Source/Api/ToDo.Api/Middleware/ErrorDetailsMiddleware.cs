using System.Net;
using ToDo.Api.Data.Exceptions;

namespace ToDo.Api.Middleware;

public sealed class ErrorDetailsMiddleware
{
    private const string ResponseContentType = "application/json";
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorDetailsMiddleware> _logger;

    public ErrorDetailsMiddleware(RequestDelegate next, ILogger<ErrorDetailsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = ResponseContentType;
        context.Response.StatusCode = exception switch
        {
            ToDoItemNotFoundException toDoItemNotFoundException => (int) HttpStatusCode.NotFound,
            CommitFailedException commitFailedException => (int) HttpStatusCode.InternalServerError,
            _ => (int) HttpStatusCode.InternalServerError
        };
        
        await context.Response.WriteAsync($"Responded with Message: {exception.Message}");
    }
}