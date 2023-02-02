using System.Net;
using ToDo.Infrastructure.Exceptions;

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
            _logger.LogError(ex, "Something went wrong");
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
        
        await context.Response.WriteAsync(new ErrorDetails(context.Response.StatusCode, exception.Message).ToString());
    }
}