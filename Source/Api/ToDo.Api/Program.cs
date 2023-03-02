using Serilog;
using ToDo.Api.Data;
using ToDo.Api.Data.Extensions;
using ToDo.Api.Extensions;
using ToDo.Api.Features.Extensions;
using ToDo.Api.Middleware;
using ToDo.Api.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
InfrastructureOptions iOptions = new();
ApiOptions aOptions = new();
builder.Configuration.GetSection(nameof(InfrastructureOptions)).Bind(iOptions);
builder.Configuration.GetSection(nameof(ApiOptions)).Bind(aOptions);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});
builder.Services
    .AddApplication()
    .AddData(iOptions)
    .AddApi(aOptions);

WebApplication app = builder.Build();
app.UseMiddleware<ErrorDetailsMiddleware>();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapSwagger();
app.Run();