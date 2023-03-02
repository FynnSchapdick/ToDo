using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;
using ToDo.Api.Data.Repositories;
using ToDo.Api.Data.Repositories.Abstractions;
using ToDo.Api.Data.SieveProcessors;

namespace ToDo.Api.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection serviceCollection, InfrastructureOptions infrastructureOptions)
    {
        serviceCollection.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddDbContext<ToDoContext>(options =>
        {
            options.UseNpgsql(infrastructureOptions.ToDoDb);
        });
        serviceCollection.AddScoped<ISieveProcessor, ToDoItemSieveProcessor>();
        serviceCollection.AddScoped<IToDoViewProvider, ToDoViewProvider>();
        return serviceCollection;
    }
}