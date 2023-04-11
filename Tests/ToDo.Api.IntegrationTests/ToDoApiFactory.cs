using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDo.Api.Core.Domain;
using ToDo.Api.Data;
using ToDo.Client;

namespace ToDo.Api.IntegrationTests;

public sealed class ToDoApiFactory : WebApplicationFactory<AssemblyMarker>
{
    private const string ServerBaseurl = "http://localhost:80";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {  
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(ToDoContext));
            services.RemoveAll<DbContextOptions<ToDoContext>>();
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseInMemoryDatabase("testdb");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            services.AddRefitClient<IToDoClient>(_ =>  new RefitSettings())
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(ServerBaseurl));
        });
        base.ConfigureWebHost(builder);
    }

    public async Task ArrangeForEndpointTesting(params ToDoItem[] toDoItems)
    {
        await using AsyncServiceScope scope = Services.CreateAsyncScope();
        ToDoContext context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
        context.ToDoItems.AddRange(toDoItems);
        await context.SaveChangesAsync();
    }
}