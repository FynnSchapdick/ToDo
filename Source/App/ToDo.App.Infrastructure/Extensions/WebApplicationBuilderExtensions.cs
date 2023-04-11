using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ToDo.App.Core.Abstractions;
using ToDo.App.Infrastructure.Services;
using ToDo.Client;

namespace ToDo.App.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string ToDoApiUrl = "Apis:ToDo:Url";
    
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<IToDoClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>(ToDoApiUrl)));
        builder.Services.AddScoped<IToDoService, ToDoService>();
        return builder;
    }
}