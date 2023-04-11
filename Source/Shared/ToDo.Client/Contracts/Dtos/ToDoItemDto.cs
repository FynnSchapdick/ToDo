namespace ToDo.Client.Contracts.Dtos;

public sealed record ToDoItemDto(Guid ToDoItemId, string Text, string Status);