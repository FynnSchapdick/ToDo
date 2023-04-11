using MudBlazor.Services;

namespace ToDo.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMudServices();
        //builder.WebHost.UseUrls("http://localhost:5120/");
        return builder;
    }
}