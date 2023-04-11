namespace ToDo.App.Core.Domain;

public sealed record ToDoItem
{
    public Guid Id { get; }
    public string Text { get; set; } = string.Empty;
    public string Status { get; set; }

    public ToDoItem(Guid id, string text, string? status = null)
    {
        Id = id;
        Text = text;
        Status = status ?? "Incomplete";
    }
}