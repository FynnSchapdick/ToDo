namespace ToDo.Api.Data.UnitTests;

public sealed class MigrationTests
{
    private readonly DbContextOptions<ToDoContext> _options;
    
    public MigrationTests()
    {
        // Set up an in-memory database with a random name
        _options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
    
    // [Fact]
    // public void CanApplyMigration()
    // {
    //     // Arrange
    //     using var context = new ToDoContext(_options);
    //
    //     // Act
    //     context.Database.Migrate();
    //
    //     // Assert
    //     bool tableExists = context.Model.FindEntityType("todoitems") != null;
    //     Assert.True(tableExists);
    // }
}