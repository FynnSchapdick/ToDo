using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data;
using ToDo.Api.Data.Extensions;

var webApplication = WebApplication.CreateBuilder(args)
    .AddData()
    .Build();

using var scope = webApplication.Services.CreateScope();
var toDoContext = scope.ServiceProvider.GetRequiredService<ToDoContext>();
await toDoContext.Database.EnsureCreatedAsync();
await toDoContext.Database.MigrateAsync();