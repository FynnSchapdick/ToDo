using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Exceptions;
using ToDo.Api.Contracts.Requests;
using ToDo.Api.Middleware;
using ToDo.Api.Validators;

namespace ToDo.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Console();
        });
        builder.Services.AddTransient<ErrorDetailsMiddleware>();
        builder.Services.AddValidatorsFromAssembly(typeof(PostToDoItemRequestValidator).Assembly);
        return builder;
    }
}