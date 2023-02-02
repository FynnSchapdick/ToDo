using ToDo.Core.Domain.Exceptions;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Core.Domain;

public sealed class ToDoItem
{
    public Guid Id { get; }
    public string Text { get; private set; }
    public bool Completed { get; private set; }
    private ToDoItem(Guid id, string text, bool completed = false)
    {
        Id = id;
        Text = text;
        Completed = completed;
    }
    
    public void UpdateText(string text)
    {
        if (text.Length is < 5 or > 255)
        {
            throw new ToDoItemException(nameof(UpdateText), text);
        }

        Text = text;
    }
    public void ToggleComplete()
    {
        Completed = !Completed;
    }
    
    private static bool TextNotValid(string text)
        => text.Length is < 5 or > 255;

    private static bool GuidNotValid(Guid id)
        => id == Guid.Empty;
    
    public static ToDoItem Create(string text)
        => TextNotValid(text)
            ? throw new ToDoItemException(nameof(Create), text)
            : Init(Guid.NewGuid(), text);

    public static ToDoItem Init(Guid id, string text, bool completed = false)
    {
        if (GuidNotValid(id))
        {
            throw new ToDoItemException(nameof(Init), id);
        }
        
        if (TextNotValid(text))
        {
            throw new ToDoItemException(nameof(Init), text);
        }

        return new ToDoItem(id, text, completed);
    }
    public static ToDoItemDto AsDto(ToDoItem toDoItem)
        => new(toDoItem.Id, toDoItem.Text, toDoItem.Completed);
}

