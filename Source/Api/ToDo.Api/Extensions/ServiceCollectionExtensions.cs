using FluentValidation;
using Microsoft.OpenApi.Models;
using ToDo.Api.Options;
using ToDo.Shared.Contracts.V1.Validators;

namespace ToDo.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection serviceCollection, ApiOptions apiOptions)
    {
        //serviceCollection.AddValidatorsFromAssemblyContaining<CreateToDoItemRequestValidator>();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(apiOptions.Name, new OpenApiInfo { Title = apiOptions.Title, Version = apiOptions.Version });
            c.EnableAnnotations();
        });
        serviceCollection.AddControllers();
        serviceCollection.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        return serviceCollection;
    }
}