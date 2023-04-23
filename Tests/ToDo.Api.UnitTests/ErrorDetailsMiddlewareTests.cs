namespace ToDo.Api.UnitTests;

public sealed class ErrorDetailsMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_ShouldCallNext_WhenNoExceptionThrown()
    {
        // Arrange
        ErrorDetailsMiddleware middleware = new ErrorDetailsMiddleware();
        DefaultHttpContext context = new DefaultHttpContext();
        bool nextCalled = false;

        // Act
        await middleware.InvokeAsync(context, _ =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        });

        // Assert
        Assert.True(nextCalled);
    }

    [Fact]
    public async Task InvokeAsync_ShouldWriteExceptionDetailsToResponse()
    {
        // Arrange
        ErrorDetailsMiddleware middleware = new ErrorDetailsMiddleware();
        DefaultHttpContext context = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };
        Exception exception = new Exception("Test Exception");

        // Act
        await middleware.InvokeAsync(context, _ => throw exception);
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
        
        // Assert
        Assert.Contains($"Responded with Message: {exception.Message} and Stacktrace: {exception.StackTrace}", body);
        Assert.Equal((int) HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/json", context.Response.ContentType);
    }
}