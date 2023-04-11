using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDo.App.Core.Abstractions;
using ToDo.App.IntegrationTests.Fakes;
using ToDo.Client;
using Withywoods.WebTesting;

namespace ToDo.App.IntegrationTests;

public sealed class ToDoAppFactory : WebApplicationFactoryFixture<AssemblyMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IToDoClient));
            services.RemoveAll(typeof(IToDoService));
            services.AddScoped<IToDoService, FakeToDoService>();
        });
        base.ConfigureWebHost(builder);
    }
}