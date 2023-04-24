using MudBlazor.Services;
using ToDo.App.Infrastructure.Configuration;

namespace ToDo.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMudServices();
        builder.Services.ConfigureOptions<ApiOptionsConfiguration>();
        return builder;
    }
}