using Microsoft.EntityFrameworkCore;
using ToDo.Api.Core.Domain;

namespace ToDo.Api.Data;

public class ToDoContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoItemConfiguration).Assembly);
    }
}