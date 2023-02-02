using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Framework.ToDoClient.Validators;

namespace ToDo.Framework.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFramework(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssemblyContaining<CreateToDoItemRequestValidator>();
        return serviceCollection;
    }
}