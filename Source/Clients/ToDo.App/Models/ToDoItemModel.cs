namespace ToDo.App.Models;

public sealed class ToDoItemModel
{
    public Guid Id { get; }
    public string Text { get; set; }
    public bool Completed { get; set; }

    public ToDoItemModel(Guid id, string text, bool completed)
    {
        Id = id;
        Text = text;
        Completed = completed;
    }
}