using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ToDo.App.Infrastructure.Configuration;

public sealed class ApiOptionsConfiguration : IConfigureOptions<ApiOptions>
{
    private readonly IConfiguration _configuration;

    public ApiOptionsConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(ApiOptions options)
    {
        options.Url = _configuration.GetValue<string>("API_URL") ?? throw new ArgumentException("Missing API_URL");
    }
}