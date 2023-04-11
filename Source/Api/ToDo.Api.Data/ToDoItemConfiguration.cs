using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Api.Core.Domain;

namespace ToDo.Api.Data;

public sealed class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    private const string TodoItemTable = "todoitems";
    private const string TodoItemPrimaryKey = "pk_todoitems";
    private const string TodoItemTextIndex = "ix_text";
    private const string TodoItemIdColumn = "id";
    private const int TodoItemTextMaxLength = 255;
    private const string TodoItemTextColumnName = "text";
    private const string TodoItemStatusColumn = "status";

    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.ToTable(TodoItemTable);

        builder.HasKey(x => x.ToDoItemId)
            .HasName(TodoItemPrimaryKey);

        builder.Property(x => x.ToDoItemId)
            .HasColumnName(TodoItemIdColumn);

        builder.Property(x => x.Text)
            .HasMaxLength(TodoItemTextMaxLength)
            .HasColumnName(TodoItemTextColumnName);

        builder.Property(x => x.Status)
            .HasColumnName(TodoItemStatusColumn);

        builder.HasIndex(x => x.Text)
            .IsUnique()
            .HasDatabaseName(TodoItemTextIndex);
    }
}