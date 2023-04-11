using ToDo.App.Extensions;
using ToDo.App.Infrastructure.Extensions;

try
{
    WebApplication.CreateBuilder(args)
        .AddApp()
        .AddInfrastructure()
        .Build()
        .UseApp()
        .Run();
}
catch (Exception ex)
{
}

