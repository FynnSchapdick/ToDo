using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDo.Api.Data;
using ToDo.Api.Data.Entities;
using ToDo.App.Infrastructure.Services.Abstractions;

namespace ToDo.Api.IntegrationTests;

public sealed class ToDoApiFactory : WebApplicationFactory<AssemblyMarker>, IAsyncLifetime
{
    private const string DatabaseName = "integrationTestDb";
    private const string DatabaseUsername = "Testuser";
    private const string DatabasePassword = "testpassword";
    private const string ServerBaseurl = "http://localhost:80";

    private readonly PostgreSqlTestcontainer _dbContainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
        .WithAutoRemove(true) 
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = DatabaseName,
            Username = DatabaseUsername,
            Password = DatabasePassword
        }).Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {  
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(ToDoContext));
            services.RemoveAll<DbContextOptions<ToDoContext>>();
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseNpgsql(_dbContainer.ConnectionString);
            });
            services.AddRefitClient<IToDoClient>(_ => new RefitSettings())
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(ServerBaseurl));
        });
        base.ConfigureWebHost(builder);
    }

    public async Task InsertToDoItemForEndpointTesting(params ToDoItemEntity[] toDoItems)
    {
        await using AsyncServiceScope scope = Services.CreateAsyncScope();
        ToDoContext context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
        await context.ToDoItems.AddRangeAsync(toDoItems);
        await context.SaveChangesAsync();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await using var asyncScope = Services.CreateAsyncScope();
        ToDoContext context = asyncScope.ServiceProvider.GetRequiredService<ToDoContext>();
        await context.Database.EnsureCreatedAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}