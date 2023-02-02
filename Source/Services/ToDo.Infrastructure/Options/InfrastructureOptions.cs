using Sieve.Models;

namespace ToDo.Infrastructure.Options;

public sealed class InfrastructureOptions
{
    public string ToDoDb { get; set; } = null!;

    public SieveOptions Sieve { get; set; } = null!;
}