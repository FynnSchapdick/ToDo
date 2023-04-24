using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using ToDo.App.Core.Abstractions;
using ToDo.App.Infrastructure.Configuration;
using ToDo.App.Infrastructure.Services;
using ToDo.Client;

namespace ToDo.App.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<IToDoClient>()
            .ConfigureHttpClient((p,c) => c.BaseAddress = new Uri(p.GetRequiredService<IOptions<ApiOptions>>().Value.Url));
        builder.Services.AddScoped<IToDoService, ToDoService>();
        return builder;
    }
}