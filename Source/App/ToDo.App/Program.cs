using ToDo.App.Extensions;
using ToDo.App.Infrastructure.Extensions;

WebApplication.CreateBuilder(args)
    .AddApp()
    .AddInfrastructure()
    .Build()
    .UseApp()
    .Run();