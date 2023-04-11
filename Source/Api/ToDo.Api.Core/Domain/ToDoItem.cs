namespace ToDo.Api.Core.Domain;

public sealed record ToDoItem
{
    public Guid ToDoItemId { get; init; } = Guid.NewGuid();
    public string Text { get; init; } = string.Empty;
    public string Status { get; init; } = "Incomplete";
    public ToDoItem(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Needed for Entity-Framework-Core
    /// </summary>
    public ToDoItem()
    {
        
    }
}

