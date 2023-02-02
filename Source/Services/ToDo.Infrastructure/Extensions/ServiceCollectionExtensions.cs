using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;
using ToDo.Core.Abstractions;
using ToDo.Infrastructure.Options;
using ToDo.Infrastructure.Repositories;
using ToDo.Infrastructure.SieveProcessors;

namespace ToDo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, InfrastructureOptions infrastructureOptions)
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