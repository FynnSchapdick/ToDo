using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Shared.Contracts.V1.Requests;
using ToDo.Shared.Contracts.V1.Validators;

namespace ToDo.Shared.Contracts.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddContracts(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<CreateToDoItemRequest>, CreateToDoItemRequestValidator>();
        serviceCollection.AddScoped<IValidator<DeleteToDoItemRequest>, DeleteToDoItemRequestValidator>();
        serviceCollection.AddScoped<IValidator<ToggleToDoItemRequest>, ToggleToDoItemRequestValidator>();
        serviceCollection.AddScoped<IValidator<UpdateToDoItemRequest>, UpdateToDoItemRequestValidator>();
        return serviceCollection;
    }
}