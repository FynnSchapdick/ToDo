namespace ToDo.Framework.ToDoClient.Contracts;

public sealed record ToDoItemDto(Guid ToDoItemId, string Text, bool Completed);