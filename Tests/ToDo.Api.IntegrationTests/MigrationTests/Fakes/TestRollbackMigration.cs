using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Api.IntegrationTests.MigrationTests.Fakes;

public partial class TestRollbackMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Add or modify database schema to represent state after migration to test
        migrationBuilder.CreateTable(
            name: "FakeTable",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false),
                Name = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FakeTable", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Rollback changes made in Up method
        migrationBuilder.DropTable(
            name: "FakeTable");
    }
}