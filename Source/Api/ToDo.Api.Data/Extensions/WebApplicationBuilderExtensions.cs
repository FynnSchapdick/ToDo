using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ToDo.Api.Core.Abstractions;

namespace ToDo.Api.Data.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "postgresdb",
            Port = GetRequiredValue<int>(builder, "POSTGRES_PORT"),
            Database = GetRequiredValue<string>(builder, "POSTGRES_DB"),
            Username = GetRequiredValue<string>(builder, "POSTGRES_USER"),
            Password = GetRequiredValue<string>(builder, "POSTGRES_PASSWORD")
        };
        builder.Services.AddDbContext<ToDoContext>(options =>
            options.UseNpgsql(connectionStringBuilder.ConnectionString));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        return builder;
    }

    private static T GetRequiredValue<T>(WebApplicationBuilder builder, string key)
    {
        return builder.Configuration.GetValue<T?>(key) ?? throw new ArgumentException($"Missing {key}");
    }
}