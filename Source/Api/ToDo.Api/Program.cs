using Serilog;
using ToDo.Api.Data.Extensions;
using ToDo.Api.Extensions;

Log.Logger = ConfigurationBuilderExtensions
    .BuildConfiguration(args)
    .CreateLogger();

try
{
    Log.Information("Starting up...");
    
    WebApplication.CreateBuilder(args)
        .AddData()
        .AddApi()
        .Build()
        .UseApi()
        .Run();
    
    Log.Information("Shutting down...");
}
catch (Exception exception)
{
    Log.Fatal(exception, "Api host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



