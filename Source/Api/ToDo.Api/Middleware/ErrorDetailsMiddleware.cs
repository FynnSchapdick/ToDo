using System.Net;
using Serilog;

namespace ToDo.Api.Middleware;

public sealed class ErrorDetailsMiddleware : IMiddleware
{
    private const string ResponseContentType = "application/json";
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.Body.Position = 0;
        context.Response.ContentType = ResponseContentType;
        context.Response.StatusCode = exception switch
        {
            _ => (int) HttpStatusCode.InternalServerError
        };

        await context.Response.WriteAsync($"Responded with Message: {exception.Message} and Stacktrace: {exception.StackTrace}");
    }
}