using ToDo.Api.Data;
using ToDo.Api.Data.Extensions;

var webApplication = WebApplication.CreateBuilder(args)
    .AddData()
    .Build();

using IServiceScope scope = webApplication.Services.CreateScope();
ToDoContext toDoContext = scope.ServiceProvider.GetRequiredService<ToDoContext>();
await toDoContext.Database.EnsureCreatedAsync();