using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data.Entities;

namespace ToDo.Api.Data;

public sealed class ToDoContext : DbContext
{
    public DbSet<ToDoItemEntity> ToDoItems { get; set; } = null!;

    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoItemConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}