using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Api.Data.Entities;

public sealed class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItemEntity>
{
    private const string TodoitemTableName = "todoitems";
    private const string TodoitemPrimaryKeyName = "pk_todoitems";
    private const string TodoitemTextIndexName = "ix_text";
    private const string TodoitemIdColumnName = "id";
    private const int TodoitemTextMaxLength = 255;
    private const string TodoitemTextColumnName = "text";
    private const string TodoitemDoneColumnName = "done";

    public void Configure(EntityTypeBuilder<ToDoItemEntity> builder)
    {
        builder.ToTable(TodoitemTableName);

        builder.HasKey(x => x.Id)
            .HasName(TodoitemPrimaryKeyName);

        builder.Property(x => x.Id)
            .HasColumnName(TodoitemIdColumnName);

        builder.Property(x => x.Text)
            .HasMaxLength(TodoitemTextMaxLength)
            .HasColumnName(TodoitemTextColumnName);

        builder.Property(x => x.Completed)
            .HasColumnName(TodoitemDoneColumnName);

        builder.HasIndex(x => x.Text)
            .IsUnique()
            .HasDatabaseName(TodoitemTextIndexName);
    }
}