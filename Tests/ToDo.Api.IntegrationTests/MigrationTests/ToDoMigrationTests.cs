using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ToDo.Api.Core.Domain;
using ToDo.Api.Data;

namespace ToDo.Api.IntegrationTests.MigrationTests;

public sealed class ToDoMigrationTests
{
    [Fact]
    public void Up_TestMigration()
    {
        // Arrange
        SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder
        {
            DataSource = ":memory:"
        };
        SqliteConnection connection = new SqliteConnection(connectionStringBuilder.ToString());
        connection.Open();

        DbContextOptions<ToDoContext> options = new DbContextOptionsBuilder<ToDoContext>()
            .UseSqlite(connection)
            .Options;

        // Act
        using var context = new ToDoContext(options);
        IMigrator migrator = context.Database.GetService<IMigrator>();
        migrator.Migrate();

        // Assert
        IEntityType? entityType = context.Model.FindEntityType(typeof(ToDoItem));
        Assert.NotNull(entityType);
        Assert.Equal("todoitems", entityType.GetTableName());

        List<string> columns = entityType
            .GetProperties()
            .Where(p => !p.IsShadowProperty())
            .Select(p => p.Name)
            .ToList();

        Assert.Contains("ToDoItemId", columns);
        Assert.Contains("Text", columns);
        Assert.Contains("Status", columns);

        IKey? primaryKey = entityType.FindPrimaryKey();
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties, p => p.Name == "ToDoItemId");

        IProperty? textProperty = entityType.FindProperty("Text");
        Assert.NotNull(textProperty);
        Assert.Equal(255, textProperty.GetMaxLength());
    }
}