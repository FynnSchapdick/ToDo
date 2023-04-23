using Microsoft.AspNetCore.Http.Extensions;

namespace ToDo.Api.Extensions;

public static class HttpContextExtensions
{
    public static string BuildRouteLocationHeaderUri(this HttpContext context, string route, string id)
        => UriHelper.BuildAbsolute(context.Request.Scheme, context.Request.Host, context.Request.PathBase, $"/{route}/{id}");
}