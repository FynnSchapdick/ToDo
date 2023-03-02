using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Api.Features.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateToDoItemCommandHandler).Assembly));
        return serviceCollection;
    }
}