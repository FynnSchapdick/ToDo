using ToDo.Api.Data.Extensions;
using ToDo.Api.Extensions;

WebApplication.CreateBuilder(args)
    .AddApi()
    .AddData()
    .Build()
    .UseApi()
    .Run();



