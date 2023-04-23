using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;
using ToDo.Api.Core.Abstractions;

namespace ToDo.Api.Data.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ToDoContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoDb")));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        return builder;
    }
}