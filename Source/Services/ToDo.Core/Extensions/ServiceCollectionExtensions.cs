using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Core.Application.Features;

namespace ToDo.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(typeof(CreateToDoItemCommandHandler).Assembly);
        return serviceCollection;
    }
}