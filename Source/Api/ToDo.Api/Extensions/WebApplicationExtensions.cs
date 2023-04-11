using Serilog;
using ToDo.Api.Endpoints;
using ToDo.Api.Middleware;

namespace ToDo.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseMiddleware<ErrorDetailsMiddleware>();
        app.MapPostToDoItemEndpoints();
        app.MapDeleteToDoItemEndpoints();
        app.MapPatchToDoItemStatusEndpoints();
        app.MapPatchToDoItemTextEndpoints();
        app.MapGetPaginatedToDoItemsEndpoints();
        return app;
    }
}