using Sieve.Models;

namespace ToDo.Api.Data;

public sealed class InfrastructureOptions
{
    public string ToDoDb { get; set; } = null!;

    public SieveOptions Sieve { get; set; } = null!;
}