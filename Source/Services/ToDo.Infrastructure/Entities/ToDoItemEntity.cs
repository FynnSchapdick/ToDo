using ToDo.Core.Domain;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Infrastructure.Entities;

public sealed class ToDoItemEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool Completed { get; set; }

    public ToDoItemEntity() { }
    private ToDoItemEntity(Guid id, string text, bool completed = false)
    {
        Id = id;
        Text = text;
        Completed = completed;
    }

    public ToDoItem AsModel() => ToDoItem.Init(Id, Text, Completed);

    public static ToDoItemEntity Create(string text)
        => new(Guid.NewGuid(), text);

    public static ToDoItemEntity Init(ToDoItem model)
        => new(model.Id, model.Text, model.Completed);

    // public static ToDoItemDto AsDto(ToDoItemEntity entity)
    //     => new(entity.Id, entity.Text, entity.Completed);
}