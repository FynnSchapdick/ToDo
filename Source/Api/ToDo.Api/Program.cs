using Serilog;
using ToDo.Api.Data.Extensions;
using ToDo.Api.Extensions;

Log.Logger = ConfigurationBuilderExtensions
    .BuildConfiguration(args)
    .CreateLogger();

WebApplication.CreateBuilder(args)
    .AddData()
    .AddApi()
    .Build()
    .UseApi()
    .Run();



