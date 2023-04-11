using Serilog;
using Serilog.Core;
using Serilog.Exceptions;

namespace ToDo.Api.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfiguration BuildConfiguration(string[] args) =>
        new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
        .AddCommandLine(args)
        .AddEnvironmentVariables()
        .Build();

    public static Logger CreateLogger(this IConfiguration configuration) =>
        new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .CreateLogger();
}